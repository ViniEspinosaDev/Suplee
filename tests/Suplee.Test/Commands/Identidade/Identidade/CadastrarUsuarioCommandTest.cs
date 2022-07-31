using Moq;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Identidade.Events;
using Suplee.Test.Builder.Commands.Identidade.Identidade;
using System.Threading.Tasks;
using Xunit;

namespace Suplee.Test.Commands.Identidade.Identidade
{
    public class CadastrarUsuarioCommandTest : IdentidadeCommandTestBase
    {
        #region Validações comando
        [Fact]
        public void Deve_Validar_Comando_Valido()
        {
            var comando = new CadastrarUsuarioCommandBuilder().ComandoValido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Empty(resultadoValidacao.Errors);
        }

        [Fact]
        public void Deve_Validar_Comando_Invalido()
        {
            var comando = new CadastrarUsuarioCommandBuilder().ComandoInvalido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Equal(10, resultadoValidacao.Errors.Count);
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoNome"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoEmail"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoCPF"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoSenha"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoConfirmacaoSenha"));
        }
        #endregion

        #region Validações de negócios
        [Fact]
        public async Task Deve_Validar_Handler_Comando_Invalido()
        {
            var comando = new CadastrarUsuarioCommandBuilder().ComandoInvalido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Senhas_Diferentes()
        {
            var comando = new CadastrarUsuarioCommandBuilder().ComandoValido()
                .ComSenhas("@Senha8080", "@Senha8181").Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "As senhas não são iguais")), Times.Once);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Usuario_CPF_Ja_Existente()
        {
            var comando = new CadastrarUsuarioCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ExisteUsuarioComCPF(It.IsAny<string>())).Returns(true);

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Este CPF já está sendo usado por outro usuário")), Times.Once);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Usuario_Email_Ja_Existente()
        {
            var comando = new CadastrarUsuarioCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ExisteUsuarioComEmail(It.IsAny<string>())).Returns(true);

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Este E-mail já está sendo usado por outro usuário")), Times.Once);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Deve_Nao_Cadastrar_Usuario()
        {
            var comando = new CadastrarUsuarioCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(false));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Não foi possível cadastrar usuário")), Times.Once);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Deve_Cadastrar_Usuario()
        {
            var comando = new CadastrarUsuarioCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _mediatorHandler.Verify(x => x.PublicarDomainEvent(It.IsAny<UsuarioCadastradoEvent>()), Times.Once);

            Assert.NotNull(resultado);
        }
        #endregion
    }
}
