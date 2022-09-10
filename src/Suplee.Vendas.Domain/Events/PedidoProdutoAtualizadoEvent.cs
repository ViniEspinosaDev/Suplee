using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Events
{
    public class PedidoProdutoAtualizadoEvent : Event
    {
        public Guid UsuarioId { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }

        public PedidoProdutoAtualizadoEvent(Guid usuarioId, Guid pedidoId, Guid produtoId, int quantidade)
        {
            AggregateId = pedidoId;
            UsuarioId = usuarioId;
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }
    }
}