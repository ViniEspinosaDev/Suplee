using Suplee.Pagamentos.Domain.Enums;
using Suplee.Pagamentos.Domain.Interfaces;
using Suplee.Pagamentos.Domain.Models;

namespace Suplee.Pagamentos.AntiCorruption
{
    public class PagamentoCartaoCreditoFacade : IPagamentoCartaoCreditoFacade
    {
        private readonly IPayPalGateway _payPalGateway;
        private readonly IConfigurationManager _configManager;

        public PagamentoCartaoCreditoFacade(IPayPalGateway payPalGateway, IConfigurationManager configManager)
        {
            _payPalGateway = payPalGateway;
            _configManager = configManager;
        }

        public Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento)
        {
            var apiKey = _configManager.GetValue("apiKey");
            var encriptionKey = _configManager.GetValue("encriptionKey");

            //var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
            //var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, pagamento.NumeroCartao);

            //var pagamentoResult = _payPalGateway.CommitTransaction(cardHashKey, pedido.Id.ToString(), pagamento.Valor);
            var pagamentoResult = _payPalGateway.CommitTransaction(pagamento.SucessoNaTransacao);

            // TODO: O gateway de pagamentos que deve retornar o objeto transação
            var transacao = new Transacao
            {
                PedidoId = pedido.Id,
                //Total = pedido.Valor,
                PagamentoId = pagamento.Id
            };

            if (pagamentoResult)
            {
                transacao.StatusTransacao = EStatusTransacao.Pago;
                return transacao;
            }

            transacao.StatusTransacao = EStatusTransacao.Recusado;
            return transacao;
        }
    }
}