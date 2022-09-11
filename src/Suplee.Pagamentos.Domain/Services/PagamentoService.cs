using Suplee.Core.Communication.Mediator;
using Suplee.Core.DomainObjects.DTO;
using Suplee.Core.Messages.CommonMessages.IntegrationEvents;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Pagamentos.Domain.Enums;
using Suplee.Pagamentos.Domain.Interfaces;
using Suplee.Pagamentos.Domain.Models;
using System.Threading.Tasks;

namespace Suplee.Pagamentos.Domain.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
        private readonly IMediatorHandler _mediatorHandler;

        public PagamentoService(IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
                                IMediatorHandler mediatorHandler)
        {
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido)
        {
            var pedido = new Pedido
            {
                Id = pagamentoPedido.PedidoId
            };

            var pagamento = new Pagamento(pagamentoPedido.PedidoId, pagamentoPedido.SucessoNaTransacao);

            var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

            if (transacao.StatusTransacao == EStatusTransacao.Pago)
            {
                await _mediatorHandler.PublicarEvento(new PagamentoRealizadoEvent(pedido.Id, pagamentoPedido.UsuarioId, transacao.PagamentoId, transacao.Id));

                return transacao;
            }

            await _mediatorHandler.PublicarNotificacao(new DomainNotification("pagamento", "A operadora recusou o pagamento"));
            await _mediatorHandler.PublicarEvento(new PagamentoRecusadoEvent(pedido.Id, pagamentoPedido.UsuarioId, transacao.PagamentoId, transacao.Id));

            return transacao;
        }
    }
}