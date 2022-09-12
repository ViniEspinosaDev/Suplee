using MediatR;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Interfaces.Services;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Events
{
    public class CatalogoEventHandler :
        INotificationHandler<ProdutoAdicionadoEvent>,
        INotificationHandler<PedidoIniciadoEvent>,
        INotificationHandler<PedidoProcessamentoCanceladoEvent>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IEstoqueService _estoqueService;

        public CatalogoEventHandler(IProdutoRepository produtoRepository, IMediatorHandler mediatorHandler, IEstoqueService estoqueService)
        {
            _produtoRepository = produtoRepository;
            _mediatorHandler = mediatorHandler;
            _estoqueService = estoqueService;
        }

        public async Task Handle(ProdutoAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            _produtoRepository.Adicionar(notification.Produto);

            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task Handle(PedidoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            var result = await _estoqueService.DebitarListaProdutosPedido(notification.Pedido);

            if (result)
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueConfirmadoEvent(notification.Pedido.PedidoId, notification.UsuarioId, notification.Pedido, notification.SucessoNaTransacao));
            }
            else
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueRejeitadoEvent(notification.Pedido.PedidoId, notification.UsuarioId));
            }
        }

        public async Task Handle(PedidoProcessamentoCanceladoEvent notification, CancellationToken cancellationToken)
        {
            await _estoqueService.ReporListaProdutosPedido(notification.Pedido);
        }
    }
}
