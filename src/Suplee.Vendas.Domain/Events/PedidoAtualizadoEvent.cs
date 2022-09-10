using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Events
{
    public class PedidoAtualizadoEvent : Event
    {
        public Guid UsuarioId { get; private set; }
        public Guid PedidoId { get; private set; }
        public decimal ValorTotal { get; private set; }

        public PedidoAtualizadoEvent(Guid usuarioId, Guid pedidoId, decimal valorTotal)
        {
            AggregateId = pedidoId;
            UsuarioId = usuarioId;
            PedidoId = pedidoId;
            ValorTotal = valorTotal;
        }
    }
}