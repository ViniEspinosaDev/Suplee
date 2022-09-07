using System;
using System.Collections.Generic;

namespace Suplee.Core.Messages.CommonMessages.Events
{
    public class PedidoIniciadoEvent : Event
    {
        public PedidoIniciadoEvent(Guid usuarioId, Guid pedidoId, List<(Guid produtoId, int quantidade)> produtos)
        {
            UsuarioId = usuarioId;
            PedidoId = pedidoId;
            Produtos = produtos ?? new List<(Guid produtoId, int quantidade)>();
        }

        public Guid UsuarioId { get; protected set; }
        public Guid PedidoId { get; protected set; }
        public List<(Guid produtoId, int quantidade)> Produtos { get; protected set; }
    }
}
