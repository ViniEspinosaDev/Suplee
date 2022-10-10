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
    public class EditarUsuarioCommandTest : IdentidadeCommandTestBase
    {
        #region Validações comando
        [Fact]
        public void Deve_Validar_Comando_Valido()
        {
            var comando = new EditarUsuarioCommandBuilder().ComandoValido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Empty(resultadoValidacao.Errors);
        }

        [Fact]
        public void Deve_Validar_Comando_Invalido()
        {
            var comando = new EditarUsuarioCommandBuilder().ComandoInvalido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Equal(10, resultadoValidacao.Errors.Count);
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoUsuarioId"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoNome"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("Enderecos[0].ValidacaoNomeDestinatario"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("Enderecos[0].ValidacaoCEP"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("Enderecos[0].ValidacaoEstado"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("Enderecos[0].ValidacaoCidade"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("Enderecos[0].ValidacaoBairro"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("Enderecos[0].ValidacaoRua"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("Enderecos[0].ValidacaoNumero"));
        }
        #endregion

        #region Validações de negócios
        [Fact]
        public async Task Deve_Validar_Handler_Comando_Invalido()
        {
            var comando = new EditarUsuarioCommandBuilder().ComandoInvalido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Nao_Obter_Usuario_Pelo_Id()
        {
            var comando = new EditarUsuarioCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ObterPeloId(It.IsAny<Guid>())).Returns(default(Usuario));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Não existe nenhum usuário cadastrado com esse Id")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Editar_Usuario()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            var comando = new EditarUsuarioCommandBuilder().ComandoValido().Build();

            _usuarioRepository.Setup(x => x.ObterPeloId(It.IsAny<Guid>())).Returns(usuario);
            _usuarioRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _usuarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Never);

            Assert.True(resultado);
        }
        #endregion
    }
}
