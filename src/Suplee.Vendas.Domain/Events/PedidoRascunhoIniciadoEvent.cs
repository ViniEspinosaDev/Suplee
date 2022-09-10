using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Events
{
    public class PedidoRascunhoIniciadoEvent : Event
    {
        public Guid UsuarioId { get; private set; }
        public Guid PedidoId { get; private set; }

        public PedidoRascunhoIniciadoEvent(Guid usuarioId, Guid pedidoId)
        {
            AggregateId = pedidoId;
            UsuarioId = usuarioId;
            PedidoId = pedidoId;
        }
    }
}