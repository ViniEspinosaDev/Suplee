using System.ComponentModel;

namespace Suplee.Vendas.Domain.Enums
{
    public enum EPedidoStatus
    {
        [Description("Rascunho")]
        Rascunho = 0,
        [Description("Iniciado")]
        Iniciado = 1,
        [Description("Cancelado")]
        Cancelado = 2,
        [Description("Pago")]
        Pago = 3,
        [Description("Enviado")]
        Enviado = 4,
        [Description("Entregue")]
        Entregue = 5
    }
}
