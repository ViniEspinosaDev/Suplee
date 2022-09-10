using System;

namespace Suplee.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PedidoEstoqueRejeitadoEvent : IntegrationEvent
    {
        public Guid PedidoId { get; private set; }
        public Guid UsuarioId { get; private set; }

        public PedidoEstoqueRejeitadoEvent(Guid pedidoId, Guid UsuarioId)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            this.UsuarioId = UsuarioId;
        }
    }
}