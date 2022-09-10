using System;

namespace Suplee.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PagamentoRealizadoEvent : IntegrationEvent
    {
        public Guid PedidoId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid PagamentoId { get; private set; }
        public Guid TransacaoId { get; private set; }
        // public decimal Total { get; private set; }

        public PagamentoRealizadoEvent(Guid pedidoId, Guid usuarioId, Guid pagamentoId, Guid transacaoId)
        {
            AggregateId = pagamentoId;
            PedidoId = pedidoId;
            UsuarioId = usuarioId;
            PagamentoId = pagamentoId;
            TransacaoId = transacaoId;
            // Total = total;
        }
    }
}