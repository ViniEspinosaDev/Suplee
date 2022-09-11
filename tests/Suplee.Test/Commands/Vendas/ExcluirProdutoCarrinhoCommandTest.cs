using Moq;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Test.Builder.Commands.Vendas;
using Suplee.Test.Builder.Models;
using Suplee.Vendas.Domain.Events;
using Suplee.Vendas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Suplee.Test.Commands.Vendas
{
    public class ExcluirProdutoCarrinhoCommandTest : VendasCommandTestBase
    {
        #region Validações comando
        [Fact]
        public void Deve_Validar_Comando_Valido()
        {
            var comando = new ExcluirProdutoCarrinhoCommandBuilder().ComandoValido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Empty(resultadoValidacao.Errors);
        }

        [Fact]
        public void Deve_Validar_Comando_Invalido()
        {
            var comando = new ExcluirProdutoCarrinhoCommandBuilder().ComandoInvalido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Equal(2, resultadoValidacao.Errors.Count);
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoUsuarioId"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoProdutoId"));
        }
        #endregion

        #region Validações negócio
        [Fact]
        public async Task Deve_Validar_Handler_Comando_Invalido()
        {
            var comando = new ExcluirProdutoCarrinhoCommandBuilder().ComandoInvalido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Se_Cliente_Tem_Carrinho()
        {
            var comando = new ExcluirProdutoCarrinhoCommandBuilder().ComandoValido().Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(default(Pedido)));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "O usuário não possui carrinho")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Se_Produto_Existe_No_Carrinho()
        {
            var pedido = new PedidoBuilder().PadraoValido().Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(pedido));

            var comando = new ExcluirProdutoCarrinhoCommandBuilder().ComandoValido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Carrinho não possui o produto para excluir")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Excluir_Produto_Do_Carrinho()
        {
            var pedidoId = Guid.NewGuid();

            var pedidoProduto = new PedidoProdutoBuilder().PadraoValido().ComPedido(pedidoId).Build();
            var pedido = new PedidoBuilder().PadraoValido().ComProdutos(new List<PedidoProduto>() { pedidoProduto }).Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(pedido));
            _pedidoRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var comando = new ExcluirProdutoCarrinhoCommandBuilder().ComandoValido().ComProdutoId(pedidoProduto.ProdutoId).Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarEvento(It.IsAny<PedidoProdutoRemovidoEvent>()), Times.Once);

            Assert.True(resultado);
        }
        #endregion
    }
}
