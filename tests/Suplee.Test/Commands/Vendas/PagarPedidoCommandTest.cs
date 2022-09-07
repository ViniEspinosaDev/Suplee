using Moq;
using Suplee.Core.Messages;
using Suplee.Core.Messages.CommonMessages.Events;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Test.Builder.Commands.Vendas;
using Suplee.Test.Builder.Models;
using Suplee.Vendas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Suplee.Test.Commands.Vendas
{
    public class PagarPedidoCommandTest : VendasCommandTestBase
    {
        #region Validações comando
        [Fact]
        public void Deve_Validar_Comando_Valido()
        {
            var comando = new PagarPedidoCommandBuilder().ComandoValido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Empty(resultadoValidacao.Errors);
        }

        [Fact]
        public void Deve_Validar_Comando_Invalido()
        {
            var comando = new PagarPedidoCommandBuilder().ComandoInvalido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Equal(1, resultadoValidacao.Errors.Count);
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoUsuarioId"));
        }
        #endregion

        #region Validações negócio
        [Fact]
        public async Task Deve_Validar_Handler_Comando_Invalido()
        {
            var comando = new PagarPedidoCommandBuilder().ComandoInvalido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);

            Assert.False(resultado);
        }

        #endregion
        [Fact]
        public async Task Deve_Validar_Se_Cliente_Tem_Carrinho()
        {
            var comando = new PagarPedidoCommandBuilder().ComandoValido().Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(default(Pedido)));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "O usuário não possui carrinho")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Se_Carrinho_Possui_Produto()
        {
            var pedido = new PedidoBuilder().PadraoValido().Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(pedido));

            var comando = new PagarPedidoCommandBuilder().ComandoValido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "O carrinho não possui produto(s)")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Se_Comando_Veio_Com_Sucesso()
        {
            var pedidoId = Guid.NewGuid();

            var pedidoProduto = new PedidoProdutoBuilder().PadraoValido().ComPedido(pedidoId).Build();
            var pedido = new PedidoBuilder().PadraoValido().ComProdutos(new List<PedidoProduto>() { pedidoProduto }).Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(pedido));

            var comando = new PagarPedidoCommandBuilder().ComandoValido().ComFalha().ComUsuarioId(pedido.UsuarioId).Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "A operadora recusou o pagamento")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Iniciar_Pedido_E_Lancar_Evento_Debitar_Estoque()
        {
            var pedidoId = Guid.NewGuid();

            var pedidoProduto = new PedidoProdutoBuilder().PadraoValido().ComPedido(pedidoId).Build();
            var pedido = new PedidoBuilder().PadraoValido().ComProdutos(new List<PedidoProduto>() { pedidoProduto }).Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(pedido));
            _pedidoRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var comando = new PagarPedidoCommandBuilder().ComandoValido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarEvento(It.IsAny<PedidoIniciadoEvent>()), Times.Once);

            Assert.True(resultado);
        }

        // Caso dê erro o débito de estoque lançar comando para TornarPedidoUmCarrinho

        // Se der sucesso no debito de estoque lançar comando para RealizarPagamento (Outro contexto (Pagamento))

        // Dando certo o comando de pagamento do outro contexto, lançar evento de PagamentoRealizadoEvent e alterar status do pedido (Cancelado, pago ou enviado)
    }
}
