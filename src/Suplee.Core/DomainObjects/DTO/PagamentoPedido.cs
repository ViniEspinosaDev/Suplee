using System;

namespace Suplee.Core.DomainObjects.DTO
{
    public class PagamentoPedido
    {
        public Guid PedidoId { get; set; }
        public Guid UsuarioId { get; set; }
        public bool SucessoNaTransacao { get; set; }
        //public decimal Total { get; set; }
        //public string NomeCartao { get; set; }
        //public string NumeroCartao { get; set; }
        //public string ExpiracaoCartao { get; set; }
        //public string CvvCartao { get; set; }
    }
}