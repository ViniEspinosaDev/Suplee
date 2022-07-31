using Moq;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Models;
using Suplee.Test.Builder.Commands.Identidade.Autenticacao;
using Suplee.Test.Builder.Models;
using System.Threading.Tasks;
using Xunit;

namespace Suplee.Test.Commands.Identidade.Autenticacao
{
    public class RealizarLoginCPFCommandTest : AutenticacaoCommandTestBase
    {
        #region Validações comando
        [Fact]
        public void Deve_Validar_Comando_Valido()
        {
            var comando = new RealizarLoginCPFCommandBuilder().ComandoValido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Empty(resultadoValidacao.Errors);
        }

        [Fact]
        public void Deve_Validar_Comando_Invalido()
        {
            var comando = new RealizarLoginCPFCommandBuilder().ComandoInvalido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Equal(2, resultadoValidacao.Errors.Count);
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoCPF"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoSenha"));
        }
        #endregion

        #region Validações negócio
        [Fact]
        public async Task Deve_Validar_Handler_Comando_Invalido()
        {
            var comando = new RealizarLoginCPFCommandBuilder().ComandoInvalido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Deve_Nao_Encontrar_Usuario_Com_CPF()
        {
            var comando = new RealizarLoginCPFCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ExisteUsuarioComCPF(It.IsAny<string>())).Returns(false);

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Não existe nenhum usuário cadastrado com esse CPF")), Times.Once);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Deve_Nao_Encontrar_Usuario_Com_CPF_Senha()
        {
            var comando = new RealizarLoginCPFCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ExisteUsuarioComCPF(It.IsAny<string>())).Returns(true);
            _usuarioRepository.Setup(x => x.RealizarLoginCPF(It.IsAny<string>(), It.IsAny<string>())).Returns(default(Usuario));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "CPF e/ou senha inválidos")), Times.Once);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Status_Conta_Usuario_Aguardando_Confirmacao()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            var comando = new RealizarLoginCPFCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ExisteUsuarioComCPF(It.IsAny<string>())).Returns(true);
            _usuarioRepository.Setup(x => x.RealizarLoginCPF(It.IsAny<string>(), It.IsAny<string>())).Returns(usuario);

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == $@"Confirme a criação de sua conta pelo e-mail ""{usuario.Email}"" antes de fazer o login")), Times.Once);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Deve_Realizar_Login_Usuario()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            usuario.Ativar();

            var comando = new RealizarLoginCPFCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ExisteUsuarioComCPF(It.IsAny<string>())).Returns(true);
            _usuarioRepository.Setup(x => x.RealizarLoginCPF(It.IsAny<string>(), It.IsAny<string>())).Returns(usuario);

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Never);

            Assert.NotNull(resultado);
        }
        #endregion
    }
}
