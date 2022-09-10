using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Commands
{
    public class FinalizarPedidoCommand : Command<bool>
    {
        public Guid PedidoId { get; private set; }
        public Guid UsuarioId { get; private set; }

        public FinalizarPedidoCommand(Guid pedidoId, Guid usuarioId)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            UsuarioId = usuarioId;
        }
    }
}