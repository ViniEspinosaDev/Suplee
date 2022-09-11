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
    public class AtualizarProdutoCarrinhoCommandTest : VendasCommandTestBase
    {
        #region Validações comando
        [Fact]
        public void Deve_Validar_Comando_Valido()
        {
            var comando = new AtualizarProdutoCarrinhoCommandBuilder().ComandoValido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Empty(resultadoValidacao.Errors);
        }

        [Fact]
        public void Deve_Validar_Comando_Invalido()
        {
            var comando = new AtualizarProdutoCarrinhoCommandBuilder().ComandoInvalido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Equal(3, resultadoValidacao.Errors.Count);
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoUsuarioId"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoProdutoId"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoQuantidade"));
        }
        #endregion

        #region Validações negócio
        [Fact]
        public async Task Deve_Validar_Handler_Comando_Invalido()
        {
            var comando = new AtualizarProdutoCarrinhoCommandBuilder().ComandoInvalido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Se_Cliente_Tem_Carrinho()
        {
            var comando = new AtualizarProdutoCarrinhoCommandBuilder().ComandoValido().Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(default(Pedido)));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "O usuário não possui carrinho")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Se_Existe_Produto_Para_Atualizar_Carrinho()
        {
            var pedido = new PedidoBuilder().PadraoValido().Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(pedido));

            var comando = new AtualizarProdutoCarrinhoCommandBuilder().ComandoValido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Carrinho não possui o produto para atualizar")), Times.Once);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Atualizar_Quantidade_Produto()
        {
            var pedidoId = Guid.NewGuid();

            var pedidoProduto = new PedidoProdutoBuilder().PadraoValido().ComPedido(pedidoId).Build();
            var pedido = new PedidoBuilder().PadraoValido().ComProdutos(new List<PedidoProduto>() { pedidoProduto }).Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(pedido));
            _pedidoRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var comando = new AtualizarProdutoCarrinhoCommandBuilder().ComandoValido().ComProdutoId(pedidoProduto.ProdutoId).Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarEvento(It.IsAny<PedidoProdutoAtualizadoEvent>()), Times.Once);

            Assert.True(resultado);
        }
        #endregion
    }
}
