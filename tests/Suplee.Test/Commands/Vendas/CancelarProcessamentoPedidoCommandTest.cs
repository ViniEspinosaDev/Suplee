using Moq;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Test.Builder.Commands.Vendas;
using Suplee.Test.Builder.Models;
using Suplee.Vendas.Domain.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Suplee.Test.Commands.Vendas
{
    public class CancelarProcessamentoPedidoCommandTest : VendasCommandTestBase
    {
        #region Validações comando
        [Fact]
        public void Deve_Validar_Comando_Valido()
        {
            var comando = new CancelarProcessamentoPedidoCommandBuilder().ComandoValido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Empty(resultadoValidacao.Errors);
        }

        [Fact]
        public void Deve_Validar_Comando_Invalido()
        {
            var comando = new CancelarProcessamentoPedidoCommandBuilder().ComandoInvalido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Equal(2, resultadoValidacao.Errors.Count);
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoUsuarioId"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoPedidoId"));
        }
        #endregion

        #region Validações negócio
        [Fact]
        public async Task Deve_Validar_Handler_Comando_Invalido()
        {
            var comando = new CancelarProcessamentoPedidoCommandBuilder().ComandoInvalido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Que_Usuario_Nao_Possui_Pedido()
        {
            var comando = new CancelarProcessamentoPedidoCommandBuilder().ComandoValido().Build();

            _pedidoRepository.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(Task.FromResult(default(Pedido)));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Pedido não encontrado")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Tornar_Pedido_Rascunho_Com_Sucesso()
        {
            var usuarioId = Guid.NewGuid();

            var pedido = new PedidoBuilder().PadraoValido().ComUsuarioId(usuarioId).Build();

            var comando = new CancelarProcessamentoPedidoCommandBuilder().ComandoValido().ComUsuarioId(usuarioId).ComPedidoId(pedido.Id).Build();

            _pedidoRepository.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(Task.FromResult(pedido));
            _pedidoRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Never);

            Assert.True(resultado);
        }
        #endregion
    }
}
