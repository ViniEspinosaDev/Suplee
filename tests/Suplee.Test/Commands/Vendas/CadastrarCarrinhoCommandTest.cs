using Moq;
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
    public class CadastrarCarrinhoCommandTest : VendasCommandTestBase
    {
        #region Validações comando
        [Fact]
        public void Deve_Validar_Comando_Valido()
        {
            var comando = new CadastrarCarrinhoCommandBuilder().ComandoValido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Empty(resultadoValidacao.Errors);
        }

        [Fact]
        public void Deve_Validar_Comando_Invalido()
        {
            var comando = new CadastrarCarrinhoCommandBuilder().ComandoInvalido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Equal(5, resultadoValidacao.Errors.Count);
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoUsuarioId"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("Produtos[0].ValidacaoProdutoId"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("Produtos[0].ValidacaoNomeProduto"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("Produtos[0].ValidacaoQuantidade"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("Produtos[0].ValidacaoValorUnitario"));
        }
        #endregion

        #region Validações negócio
        [Fact]
        public async Task Deve_Validar_Handler_Comando_Invalido()
        {
            var comando = new CadastrarCarrinhoCommandBuilder().ComandoInvalido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Criar_Novo_Rascunho()
        {
            var comando = new CadastrarCarrinhoCommandBuilder().ComandoValido().Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(default(Pedido)));
            _pedidoRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);

            Assert.True(resultado);
        }

        [Fact]
        public async Task Deve_Recuperar_Rascunho_E_Substituir_Todos_Produtos()
        {
            var pedidoId = Guid.NewGuid();

            var produtos = new List<PedidoProduto>()
            {
                new PedidoProdutoBuilder().ComPedido(pedidoId).ComProduto(new ProdutoBuilder().PadraoValido().ComNome("Produto A").Build()).Build(),
                new PedidoProdutoBuilder().ComPedido(pedidoId).ComProduto(new ProdutoBuilder().PadraoValido().ComNome("Produto B").Build()).Build(),
                new PedidoProdutoBuilder().ComPedido(pedidoId).ComProduto(new ProdutoBuilder().PadraoValido().ComNome("Produto C").Build()).Build()
            };

            var pedido = new PedidoBuilder().PadraoValido().ComProdutos(produtos).Build();

            var comando = new CadastrarCarrinhoCommandBuilder().ComandoValido().Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(pedido));
            _pedidoRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);

            Assert.True(resultado);
        }

        #endregion
    }
}
