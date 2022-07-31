using Moq;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Core.Messages.Mail;
using Suplee.Identidade.Domain.Identidade.Events;
using Suplee.Identidade.Domain.Interfaces;
using System.Threading;

namespace Suplee.Test.Events.Identidade.Identidade
{
    public abstract class IdentidadeEventTestBase
    {
        protected readonly IdentidadeEventHandler _handler;
        protected readonly CancellationToken _cancellationToken;
        protected readonly Mock<IMediatorHandler> _mediatorHandler;
        protected readonly Mock<IUsuarioRepository> _usuarioRepository;
        protected readonly Mock<IMailService> _mailService;

        protected IdentidadeEventTestBase()
        {
            _cancellationToken = new CancellationToken();
            _mediatorHandler = new Mock<IMediatorHandler>();
            _usuarioRepository = new Mock<IUsuarioRepository>();
            _mailService = new Mock<IMailService>();

            _mediatorHandler.Setup(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()));
            _mailService.Setup(x => x.SendMailAsync(It.IsAny<Mail>()));

            _handler = new IdentidadeEventHandler(_mediatorHandler.Object, _usuarioRepository.Object, _mailService.Object);
        }
    }
}
