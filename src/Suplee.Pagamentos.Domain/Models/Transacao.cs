using Suplee.Core.DomainObjects;
using Suplee.Pagamentos.Domain.Enums;
using System;

namespace Suplee.Pagamentos.Domain.Models
{
    public class Transacao : Entity
    {
        public Guid PedidoId { get; set; }
        public Guid PagamentoId { get; set; }
        public decimal Total { get; set; }
        public EStatusTransacao StatusTransacao { get; set; }

        // EF. Rel.
        public Pagamento Pagamento { get; set; }
    }
}