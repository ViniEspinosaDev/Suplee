using Moq;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Models;
using Suplee.Test.Builder.Commands.Identidade.Autenticacao;
using Suplee.Test.Builder.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Suplee.Test.Commands.Identidade.Autenticacao
{
    public class ConfirmarCadastroCommandTest : AutenticacaoCommandTestBase
    {
        #region Validações comando
        [Fact]
        public void Deve_Validar_Comando_Valido()
        {
            var comando = new ConfirmarCadastroCommandBuilder().ComandoValido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Empty(resultadoValidacao.Errors);
        }

        [Fact]
        public void Deve_Validar_Comando_Invalido()
        {
            var comando = new ConfirmarCadastroCommandBuilder().ComandoInvalido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Equal(3, resultadoValidacao.Errors.Count);
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoUsuarioId"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoCodigoConfirmacao"));
        }
        #endregion

        #region Validações negócio
        [Fact]
        public async Task Deve_Validar_Handler_Comando_Invalido()
        {
            var comando = new ConfirmarCadastroCommandBuilder().ComandoInvalido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Nao_Obter_Usuario_Pelo_Id()
        {
            var comando = new ConfirmarCadastroCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ObterPeloId(It.IsAny<Guid>())).Returns(default(Usuario));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Não existe nenhum usuário cadastrado com esse Id")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Obter_Usuario_Ativo()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            usuario.Ativar();

            var comando = new ConfirmarCadastroCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ObterPeloId(It.IsAny<Guid>())).Returns(usuario);

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Esse usuário já está ativo. Tente realizar o login")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Nao_Obter_Confirmacao_Usuario()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            var comando = new ConfirmarCadastroCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ObterPeloId(It.IsAny<Guid>())).Returns(usuario);
            _usuarioRepository.Setup(x => x.ObterConfirmacaoUsuario(It.IsAny<Guid>(), It.IsAny<string>())).Returns(default(ConfirmacaoUsuario));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Usuário e/ou código de confirmação inválidos")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Obter_Confirmacao_Usuario_Ja_Confirmada()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();
            var confirmacaoUsuario = new ConfirmacaoUsuarioBuilder().PadraoValido().Build();

            confirmacaoUsuario.Confirmar();

            var comando = new ConfirmarCadastroCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ObterPeloId(It.IsAny<Guid>())).Returns(usuario);
            _usuarioRepository.Setup(x => x.ObterConfirmacaoUsuario(It.IsAny<Guid>(), It.IsAny<string>())).Returns(confirmacaoUsuario);

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Este código de confirmação já foi confirmado")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Confirmar_Cadastro_Usuario()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();
            var confirmacaoUsuario = new ConfirmacaoUsuarioBuilder().PadraoValido().Build();

            var comando = new ConfirmarCadastroCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ObterPeloId(It.IsAny<Guid>())).Returns(usuario);
            _usuarioRepository.Setup(x => x.ObterConfirmacaoUsuario(It.IsAny<Guid>(), It.IsAny<string>())).Returns(confirmacaoUsuario);
            _usuarioRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);

            Assert.True(resultado);
        }
        #endregion
    }
}
