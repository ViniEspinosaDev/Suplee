using Moq;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Autenticacao.Commands;
using Suplee.Identidade.Domain.Interfaces;
using System.Threading;

namespace Suplee.Test.Commands.Identidade.Autenticacao
{
    public abstract class AutenticacaoCommandTestBase
    {
        protected readonly AutenticacaoCommandHandler _handler;
        protected readonly CancellationToken _cancellationToken;
        protected readonly Mock<IMediatorHandler> _mediatorHandler;
        protected readonly Mock<IUsuarioRepository> _usuarioRepository;

        protected AutenticacaoCommandTestBase()
        {
            _cancellationToken = new CancellationToken();
            _mediatorHandler = new Mock<IMediatorHandler>();
            _usuarioRepository = new Mock<IUsuarioRepository>();

            _mediatorHandler.Setup(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()));

            _handler = new AutenticacaoCommandHandler(_mediatorHandler.Object, _usuarioRepository.Object);
        }
    }
}
