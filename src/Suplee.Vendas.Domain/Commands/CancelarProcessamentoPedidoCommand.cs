using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Commands
{
    public class CancelarProcessamentoPedidoCommand : Command<bool>
    {
        public Guid PedidoId { get; private set; }
        public Guid UsuarioId { get; private set; }

        public CancelarProcessamentoPedidoCommand(Guid pedidoId, Guid usuarioId)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            UsuarioId = usuarioId;
        }
    }
}