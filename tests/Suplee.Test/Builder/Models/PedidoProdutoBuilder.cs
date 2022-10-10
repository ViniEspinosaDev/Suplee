using Suplee.Catalogo.Domain.Models;
using Suplee.Vendas.Domain.Models;
using System;

namespace Suplee.Test.Builder.Models
{
    public class PedidoProdutoBuilder : PedidoProduto
    {
        public PedidoProdutoBuilder(
            Guid produtoId = default,
            string nomeProduto = default,
            int quantidade = default,
            decimal valorUnitario = default) : base(produtoId, nomeProduto, quantidade, valorUnitario)
        {
        }

        public PedidoProdutoBuilder PadraoValido()
        {
            ProdutoId = Guid.NewGuid();
            NomeProduto = "Produto 1";
            Quantidade = 1;
            ValorUnitario = 12.59m;

            return this;
        }

        public PedidoProdutoBuilder ComPedido(Guid pedidoId)
        {
            AssociarPedido(pedidoId);

            return this;
        }

        public PedidoProdutoBuilder ComProduto(Catalogo.Domain.Models.Produto produto)
        {
            ProdutoId = produto.Id;
            NomeProduto = produto.Nome;
            Quantidade = produto.QuantidadeDisponivel;
            ValorUnitario = produto.Preco;

            return this;
        }

        public PedidoProduto Build() => this;
    }
}
