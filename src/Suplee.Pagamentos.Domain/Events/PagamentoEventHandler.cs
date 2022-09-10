using MediatR;
using Suplee.Core.DomainObjects.DTO;
using Suplee.Core.Messages.CommonMessages.IntegrationEvents;
using Suplee.Pagamentos.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Pagamentos.Domain.Events
{
    public class PagamentoEventHandler : INotificationHandler<PedidoEstoqueConfirmadoEvent>
    {
        private readonly IPagamentoService _pagamentoService;

        public PagamentoEventHandler(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        public async Task Handle(PedidoEstoqueConfirmadoEvent notification, CancellationToken cancellationToken)
        {
            var pagamentoPedido = new PagamentoPedido
            {
                PedidoId = notification.PedidoId,
                UsuarioId = notification.UsuarioId,
                SucessoNaTransacao = notification.SucessoNaTransacao
                //Total = message.Total,
                //NomeCartao = message.NomeCartao,
                //NumeroCartao = message.NumeroCartao,
                //ExpiracaoCartao = message.ExpiracaoCartao,
                //CvvCartao = message.CvvCartao
            };

            await _pagamentoService.RealizarPagamentoPedido(pagamentoPedido);
        }
    }
}