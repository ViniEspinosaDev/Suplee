using Suplee.Pagamentos.Domain.Models;

namespace Suplee.Pagamentos.Domain.Interfaces
{
    public interface IPagamentoCartaoCreditoFacade
    {
        Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento);
    }
}