using Suplee.Core.DomainObjects;
using System;

namespace Suplee.Vendas.Domain.Models
{
    public class PedidoProduto : Entity
    {
        protected PedidoProduto() { }
        public PedidoProduto(Guid produtoId, string nomeProduto, int quantidade, decimal valorUnitario)
        {
            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public Guid PedidoId { get; protected set; }
        public Guid ProdutoId { get; protected set; }
        public string NomeProduto { get; protected set; }
        public int Quantidade { get; protected set; }
        public decimal ValorUnitario { get; protected set; }

        public Pedido Pedido { get; protected set; }

        public void AssociarPedido(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }

        public decimal CalcularValor()
        {
            return ValorUnitario * Quantidade;
        }

        public void AdicionarUnidade(int unidades)
        {
            Quantidade += unidades;
        }

        public void AtualizarQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }

        public override bool EstaValido()
        {
            return true;
        }
    }
}
