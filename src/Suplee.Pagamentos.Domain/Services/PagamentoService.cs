using Suplee.Core.Communication.Mediator;
using Suplee.Core.DomainObjects.DTO;
using Suplee.Core.Messages.CommonMessages.IntegrationEvents;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Pagamentos.Domain.Enums;
using Suplee.Pagamentos.Domain.Interfaces;
using Suplee.Pagamentos.Domain.Models;
using System.Threading.Tasks;

namespace NerdStore.Pagamentos.Business
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PagamentoService(IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
                                IPagamentoRepository pagamentoRepository,
                                IMediatorHandler mediatorHandler)
        {
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
            _pagamentoRepository = pagamentoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido)
        {
            var pedido = new Pedido
            {
                Id = pagamentoPedido.PedidoId
                //Valor = pagamentoPedido.Total
            };

            var pagamento = new Pagamento
            {
                //Valor = pagamentoPedido.Total,
                //NomeCartao = pagamentoPedido.NomeCartao,
                //NumeroCartao = pagamentoPedido.NumeroCartao,
                //ExpiracaoCartao = pagamentoPedido.ExpiracaoCartao,
                //CvvCartao = pagamentoPedido.CvvCartao,
                PedidoId = pagamentoPedido.PedidoId,
                SucessoNaTransacao = pagamentoPedido.SucessoNaTransacao
            };

            var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

            if (transacao.StatusTransacao == EStatusTransacao.Pago)
            {
                pagamento.AdicionarEvento(new PagamentoRealizadoEvent(pedido.Id, pagamentoPedido.UsuarioId, transacao.PagamentoId, transacao.Id));

                _pagamentoRepository.Adicionar(pagamento);
                _pagamentoRepository.AdicionarTransacao(transacao);

                await _pagamentoRepository.UnitOfWork.Commit();
                return transacao;
            }

            await _mediatorHandler.PublicarNotificacao(new DomainNotification("pagamento", "A operadora recusou o pagamento"));
            await _mediatorHandler.PublicarEvento(new PagamentoRecusadoEvent(pedido.Id, pagamentoPedido.UsuarioId, transacao.PagamentoId, transacao.Id));

            return transacao;
        }
    }
}