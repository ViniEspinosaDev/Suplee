using Suplee.Test.Builder.Models;
using Suplee.Test.Context;
using Suplee.Vendas.Data;
using Suplee.Vendas.Data.Repository;
using Suplee.Vendas.Domain.Interfaces;
using Suplee.Vendas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Suplee.Test.Repositories.Vendas
{
    public class PedidoRepositoryTest
    {
        private readonly VendasContext _context;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoRepositoryTest()
        {
            _context = StubContextDomain.GetDatabaseContextVendas();
            _pedidoRepository = new PedidoRepository(_context);
        }

        [Fact]
        public void Deve_Testar_Instancia_Repository()
        {
            Assert.True(_pedidoRepository != null);
        }

        [Fact]
        public async void Deve_Adicionar_Pedido()
        {
            var pedido = new PedidoBuilder().PadraoValido().Build();

            _pedidoRepository.Adicionar(pedido);

            await _pedidoRepository.UnitOfWork.Commit();

            var pedidoAdicionado = await _pedidoRepository.ObterPorId(pedido.Id);

            Assert.NotNull(pedidoAdicionado);
        }

        [Fact]
        public async void Deve_Atualizar_Produtos_No_Pedido()
        {
            var pedido = new PedidoBuilder().PadraoValido().Build();

            AdicionarPedido(pedido);

            var pedidoProduto = new PedidoProduto(Guid.NewGuid(), "Produto 1", 10, 15.70m);
            pedidoProduto.AssociarPedido(pedido.Id);

            var pedidoProduto2 = new PedidoProduto(Guid.NewGuid(), "Produto 2", 15, 16.80m);
            pedidoProduto2.AssociarPedido(pedido.Id);

            var pedidoProduto3 = new PedidoProduto(Guid.NewGuid(), "Produto 3", 20, 17.90m);
            pedidoProduto3.AssociarPedido(pedido.Id);

            _pedidoRepository.AdicionarPedidoProduto(pedidoProduto);
            _pedidoRepository.AdicionarPedidoProduto(pedidoProduto2);
            _pedidoRepository.AdicionarPedidoProduto(pedidoProduto3);

            await _pedidoRepository.UnitOfWork.Commit();

            var pedidoAtualizado = await _pedidoRepository.ObterPorId(pedido.Id);

            Assert.Equal(3, pedidoAtualizado.Produtos.Count());
        }

        [Fact]
        public async void Deve_Adicionar_Pedido_Produtos()
        {
            var pedido = new PedidoBuilder().PadraoValido().Build();

            pedido.AdicionarProduto(new PedidoProduto(Guid.NewGuid(), "Produto 1", 10, 15.70m));
            pedido.AdicionarProduto(new PedidoProduto(Guid.NewGuid(), "Produto 2", 15, 16.80m));
            pedido.AdicionarProduto(new PedidoProduto(Guid.NewGuid(), "Produto 3", 20, 17.90m));

            AdicionarPedido(pedido);

            var pedidoAdicionado = await _pedidoRepository.ObterPorId(pedido.Id);

            Assert.Equal(3, pedidoAdicionado.Produtos.Count());
        }

        [Fact]
        public async void Deve_Atualizar_Pedido_Produtos()
        {
            var produtoId = Guid.NewGuid();
            var produtoId2 = Guid.NewGuid();

            var pedido = new PedidoBuilder().PadraoValido().Build();

            var pedidoProduto1 = new PedidoProduto(produtoId, "Produto 1", 10, 20.50m);
            var pedidoProduto2 = new PedidoProduto(produtoId2, "Produto 2", 15, 21.50m);

            pedido.AdicionarProduto(pedidoProduto1);
            pedido.AdicionarProduto(pedidoProduto2);

            AdicionarPedido(pedido);

            pedidoProduto1.AtualizarQuantidade(5);
            pedidoProduto2.AtualizarQuantidade(6);

            _pedidoRepository.AtualizarPedidoProduto(pedidoProduto1);
            _pedidoRepository.AtualizarPedidoProduto(pedidoProduto2);

            await _pedidoRepository.UnitOfWork.Commit();

            var pedidoAdicionado = await _pedidoRepository.ObterPorId(pedido.Id);

            Assert.Equal(2, pedidoAdicionado.Produtos.Count());
            Assert.Equal(5, pedidoAdicionado.Produtos.FirstOrDefault(x => x.ProdutoId == produtoId).Quantidade);
            Assert.Equal(6, pedidoAdicionado.Produtos.FirstOrDefault(x => x.ProdutoId == produtoId2).Quantidade);
        }

        [Fact]
        public async void Deve_Obter_Rascunho_Por_UsuarioId()
        {
            var usuarioId = Guid.NewGuid();
            var pedidos = new List<Pedido>()
            {
                new PedidoBuilder().PadraoValido().ComUsuarioId(usuarioId).Build(),
                new PedidoBuilder().PadraoValido().ComUsuarioId(usuarioId).Iniciado().Build(),
                new PedidoBuilder().PadraoValido().ComUsuarioId(usuarioId).Cancelado().Build(),
                new PedidoBuilder().PadraoValido().ComUsuarioId(usuarioId).Pago().Build(),
                new PedidoBuilder().PadraoValido().ComUsuarioId(usuarioId).Enviado().Build(),
                new PedidoBuilder().PadraoValido().ComUsuarioId(usuarioId).Entregue().Build(),
            };

            AdicionarPedidos(pedidos);

            var rascunho = await _pedidoRepository.ObterPedidoPorUsuarioId(usuarioId);

            Assert.NotNull(rascunho);
        }

        #region Métodos auxiliares
        private void AdicionarPedido(Pedido pedido)
        {
            _pedidoRepository.Adicionar(pedido);
            _pedidoRepository.UnitOfWork.Commit();
        }

        private void AdicionarPedidos(List<Pedido> pedidos)
        {
            pedidos.ForEach(p => _pedidoRepository.Adicionar(p));
            _pedidoRepository.UnitOfWork.Commit();
        }
        #endregion
    }
}
