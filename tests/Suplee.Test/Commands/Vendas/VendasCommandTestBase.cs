using Moq;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Vendas.Domain.Commands;
using Suplee.Vendas.Domain.Interfaces;
using System.Threading;

namespace Suplee.Test.Commands.Vendas
{
    public abstract class VendasCommandTestBase
    {
        protected readonly PedidoCommandHandler _handler;
        protected readonly CancellationToken _cancellationToken;
        protected readonly Mock<IMediatorHandler> _mediatorHandler;
        protected readonly Mock<IPedidoRepository> _pedidoRepository;

        protected VendasCommandTestBase()
        {
            _cancellationToken = new CancellationToken();
            _mediatorHandler = new Mock<IMediatorHandler>();
            _pedidoRepository = new Mock<IPedidoRepository>();

            _mediatorHandler.Setup(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()));

            _handler = new PedidoCommandHandler(_mediatorHandler.Object, _pedidoRepository.Object);
        }
    }
}
