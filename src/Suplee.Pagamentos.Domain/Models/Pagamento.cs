using Suplee.Core.DomainObjects;
using System;

namespace Suplee.Pagamentos.Domain.Models
{
    public class Pagamento : Entity, IAggregateRoot
    {
        public Guid PedidoId { get; set; }
        public string Status { get; set; }

        // Atributo para definir se vai dar bom ou ruim a transação (APENAS FICTÍCIO: VALOR DEFINIDO NO FRONTEND)
        public bool SucessoNaTransacao { get; set; }
        //public decimal Valor { get; set; }

        //public string NomeCartao { get; set; }
        //public string NumeroCartao { get; set; }
        //public string ExpiracaoCartao { get; set; }
        //public string CvvCartao { get; set; }

        // EF. Rel.
        public Transacao Transacao { get; set; }
    }
}
