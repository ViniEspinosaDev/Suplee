using Moq;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Core.Messages.Mail;
using Suplee.Identidade.Domain.Identidade.Commands;
using Suplee.Identidade.Domain.Interfaces;
using System.Threading;

namespace Suplee.Test.Commands.Identidade.Identidade
{
    public abstract class IdentidadeCommandTestBase
    {
        protected readonly IdentidadeCommandHandler _handler;
        protected readonly CancellationToken _cancellationToken;
        protected readonly Mock<IMediatorHandler> _mediatorHandler;
        protected readonly Mock<IUsuarioRepository> _usuarioRepository;
        protected readonly Mock<IMailService> _mailService;

        protected IdentidadeCommandTestBase()
        {
            _cancellationToken = new CancellationToken();
            _mediatorHandler = new Mock<IMediatorHandler>();
            _usuarioRepository = new Mock<IUsuarioRepository>();
            _mailService = new Mock<IMailService>();

            _mediatorHandler.Setup(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()));
            _mailService.Setup(x => x.SendMailAsync(It.IsAny<Mail>()));

            _handler = new IdentidadeCommandHandler(_mediatorHandler.Object, _usuarioRepository.Object, _mailService.Object);
        }
    }
}
