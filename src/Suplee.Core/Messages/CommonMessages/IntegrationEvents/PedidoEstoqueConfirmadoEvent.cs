using Suplee.Core.DomainObjects.DTO;
using System;

namespace Suplee.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PedidoEstoqueConfirmadoEvent : IntegrationEvent
    {
        public PedidoEstoqueConfirmadoEvent(Guid pedidoId, Guid usuarioId, PedidoDomainObject produtosPedido, bool sucessoNaTransacao)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            UsuarioId = usuarioId;
            ProdutosPedido = produtosPedido;
            SucessoNaTransacao = sucessoNaTransacao;
        }

        public Guid PedidoId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public bool SucessoNaTransacao { get; private set; }
        public PedidoDomainObject ProdutosPedido { get; private set; }
    }
}