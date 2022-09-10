using Suplee.Core.DomainObjects;
using System;

namespace Suplee.Pagamentos.Domain.Models
{
    public class Pagamento : Entity, IAggregateRoot
    {
        public Pagamento(Guid pedidoId, bool sucessoNaTransacao)
        {
            PedidoId = pedidoId;
            SucessoNaTransacao = sucessoNaTransacao;
        }

        public Guid PedidoId { get; protected set; }
        public bool SucessoNaTransacao { get; protected set; }
    }
}
