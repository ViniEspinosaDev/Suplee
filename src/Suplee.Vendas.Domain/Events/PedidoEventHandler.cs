using MediatR;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.IntegrationEvents;
using Suplee.Vendas.Domain.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Vendas.Domain.Events
{
    public class PedidoEventHandler :
        INotificationHandler<PedidoAtualizadoEvent>,
        INotificationHandler<PedidoFinalizadoEvent>,
        INotificationHandler<PedidoProdutoAdicionadoEvent>,
        INotificationHandler<PedidoProdutoAtualizadoEvent>,
        INotificationHandler<PedidoProdutoRemovidoEvent>,
        INotificationHandler<PedidoRascunhoIniciadoEvent>,
        INotificationHandler<PedidoEstoqueRejeitadoEvent>,
        INotificationHandler<PagamentoRealizadoEvent>,
        INotificationHandler<PagamentoRecusadoEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoProdutoAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoProdutoRemovidoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoProdutoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(PedidoEstoqueRejeitadoEvent notification, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoCommand(notification.PedidoId, notification.UsuarioId));
        }

        public async Task Handle(PagamentoRealizadoEvent notification, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new FinalizarPedidoCommand(notification.PedidoId, notification.UsuarioId));
        }

        public async Task Handle(PagamentoRecusadoEvent notification, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoEstornarEstoqueCommand(notification.PedidoId, notification.UsuarioId));
        }

        public Task Handle(PedidoFinalizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}