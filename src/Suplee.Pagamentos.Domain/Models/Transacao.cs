using Suplee.Core.DomainObjects;
using Suplee.Pagamentos.Domain.Enums;
using System;

namespace Suplee.Pagamentos.Domain.Models
{
    public class Transacao : Entity
    {
        public Transacao(Guid pedidoId, Guid pagamentoId, EStatusTransacao statusTransacao)
        {
            PedidoId = pedidoId;
            PagamentoId = pagamentoId;
            StatusTransacao = statusTransacao;
        }

        public Guid PedidoId { get; protected set; }
        public Guid PagamentoId { get; protected set; }
        public EStatusTransacao StatusTransacao { get; protected set; }

        public void AlterarStatus(EStatusTransacao statusTransacao) => StatusTransacao = statusTransacao;
    }
}