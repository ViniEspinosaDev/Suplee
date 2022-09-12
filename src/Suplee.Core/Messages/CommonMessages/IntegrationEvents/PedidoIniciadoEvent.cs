using Suplee.Core.DomainObjects.DTO;
using System;

namespace Suplee.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PedidoIniciadoEvent : IntegrationEvent
    {
        public PedidoIniciadoEvent(Guid usuarioId, PedidoDomainObject pedido, bool sucessoNaTransacao)
        {
            UsuarioId = usuarioId;
            Pedido = pedido;
            SucessoNaTransacao = sucessoNaTransacao;
        }

        public Guid UsuarioId { get; protected set; }
        public bool SucessoNaTransacao { get; protected set; }
        public PedidoDomainObject Pedido { get; protected set; }
    }
}
