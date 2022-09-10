using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Events
{
    public class PedidoProdutoRemovidoEvent : Event
    {
        public Guid UsuarioId { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }

        public PedidoProdutoRemovidoEvent(Guid usuarioId, Guid pedidoId, Guid produtoId)
        {
            AggregateId = pedidoId;
            UsuarioId = usuarioId;
            PedidoId = pedidoId;
            ProdutoId = produtoId;
        }
    }
}