using Moq;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Models;
using Suplee.Test.Builder.Commands.Identidade.Identidade;
using Suplee.Test.Builder.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Suplee.Test.Commands.Identidade.Identidade
{
    public class ReenviarEmailConfirmarCadastroCommandTest : IdentidadeCommandTestBase
    {
        #region Validações comando
        [Fact]
        public void Deve_Validar_Comando_Valido()
        {
            var comando = new ReenviarEmailConfirmarCadastroCommandBuilder().ComandoValido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Empty(resultadoValidacao.Errors);
        }

        [Fact]
        public void Deve_Validar_Comando_Invalido()
        {
            var comando = new ReenviarEmailConfirmarCadastroCommandBuilder().ComandoInvalido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Equal(2, resultadoValidacao.Errors.Count);
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoCPF"));
        }
        #endregion

        #region Validações de negócios
        [Fact]
        public async Task Deve_Validar_Handler_Comando_Invalido()
        {
            var comando = new ReenviarEmailConfirmarCadastroCommandBuilder().ComandoInvalido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);

            Assert.Empty(resultado);
        }

        [Fact]
        public async Task Deve_Nao_Encontrar_Usuario_Pelo_CPF()
        {
            var comando = new ReenviarEmailConfirmarCadastroCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ObterPeloCPF(It.IsAny<string>())).Returns(default(Usuario));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Não existe nenhum usuário cadastrado com esse CPF")), Times.Once);

            Assert.Empty(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Usuario_Ativo()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            usuario.Ativar();

            var comando = new ReenviarEmailConfirmarCadastroCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ObterPeloCPF(It.IsAny<string>())).Returns(usuario);

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Esse usuário já está ativo. Tente realizar o login")), Times.Once);

            Assert.Empty(resultado);
        }

        [Fact]
        public async Task Deve_Nao_Encontrar_Confirmacao_Usuario()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            var comando = new ReenviarEmailConfirmarCadastroCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ObterPeloCPF(It.IsAny<string>())).Returns(usuario);
            _usuarioRepository.Setup(x => x.ObterConfirmacaoUsuarioAindaNaoConfirmada(It.IsAny<Guid>())).Returns(default(ConfirmacaoUsuario));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Não existe nenhuma confirmação pendente para o usuário com esse CPF")), Times.Once);

            Assert.Empty(resultado);
        }

        [Fact]
        public async Task Deve_Reenviar_Email()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();
            var confirmacaoUsuario = new ConfirmacaoUsuarioBuilder().PadraoValido().Build();

            var comando = new ReenviarEmailConfirmarCadastroCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ObterPeloCPF(It.IsAny<string>())).Returns(usuario);
            _usuarioRepository.Setup(x => x.ObterConfirmacaoUsuarioAindaNaoConfirmada(It.IsAny<Guid>())).Returns(confirmacaoUsuario);
            _usuarioRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            Assert.NotEmpty(resultado);
            Assert.Equal(usuario.Email, resultado);
        }
        #endregion
    }
}
