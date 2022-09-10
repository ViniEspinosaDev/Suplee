using Suplee.Core.DomainObjects.DTO;
using System;

namespace Suplee.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PedidoEstoqueConfirmadoEvent : IntegrationEvent
    {
        public PedidoEstoqueConfirmadoEvent(Guid pedidoId, Guid usuarioId, PedidoDomainObject produtosPedido)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            UsuarioId = usuarioId;
            ProdutosPedido = produtosPedido;
            //Total = total;
            //NomeCartao = nomeCartao;
            //NumeroCartao = numeroCartao;
            //ExpiracaoCartao = expiracaoCartao;
            //CvvCartao = cvvCartao;
        }

        public Guid PedidoId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public bool SucessoNaTransacao { get; private set; }
        public PedidoDomainObject ProdutosPedido { get; private set; }
        //public decimal Total { get; private set; }
        //public string NomeCartao { get; private set; }
        //public string NumeroCartao { get; private set; }
        //public string ExpiracaoCartao { get; private set; }
        //public string CvvCartao { get; private set; }
    }
}