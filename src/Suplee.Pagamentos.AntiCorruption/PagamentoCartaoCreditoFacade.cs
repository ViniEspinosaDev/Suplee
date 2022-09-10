using Suplee.Pagamentos.Domain.Enums;
using Suplee.Pagamentos.Domain.Interfaces;
using Suplee.Pagamentos.Domain.Models;

namespace Suplee.Pagamentos.AntiCorruption
{
    public class PagamentoCartaoCreditoFacade : IPagamentoCartaoCreditoFacade
    {
        private readonly IPayPalGateway _payPalGateway;

        public PagamentoCartaoCreditoFacade(IPayPalGateway payPalGateway)
        {
            _payPalGateway = payPalGateway;
        }

        public Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento)
        {
            var pagamentoResult = _payPalGateway.CommitTransaction(pagamento.SucessoNaTransacao);

            var transacao = new Transacao(pedido.Id, pagamento.Id, default(EStatusTransacao));

            if (pagamentoResult)
            {
                transacao.AlterarStatus(EStatusTransacao.Pago);
                return transacao;
            }

            transacao.AlterarStatus(EStatusTransacao.Recusado);
            return transacao;
        }
    }
}