using Suplee.Core.DomainObjects.DTO;
using System;

namespace Suplee.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PedidoProcessamentoCanceladoEvent : IntegrationEvent
    {
        public Guid PedidoId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public PedidoDomainObject Pedido { get; private set; }

        public PedidoProcessamentoCanceladoEvent(Guid pedidoId, Guid usuarioId, PedidoDomainObject pedido)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            UsuarioId = usuarioId;
            Pedido = pedido;
        }
    }
}