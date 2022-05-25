using Moq;
using Suplee.Catalogo.Domain.Commands;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Data;
using Suplee.Core.Messages;
using Suplee.Core.Messages.CommonMessages.DomainEvents;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.ExternalService.Imgbb.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Test.Commands.Catalogo
{
    public class CatalogoCommandBase
    {
        public abstract class ColaboradorCommandTestBase
        {
            protected readonly Mock<IProdutoRepository> _produtoRepository;
            protected readonly Mock<IImgbbService> _imgbbService;
            private readonly Mock<IServiceProvider> _serviceProvider;
            protected readonly Mock<IUnitOfWork> _uow;
            protected readonly Mock<IMediatorHandler> _mediator;
            protected readonly CancellationToken _cancellationToken;

            public ColaboradorCommandTestBase()
            {
                _uow = new Mock<IUnitOfWork>();
                _mediator = new Mock<IMediatorHandler>();
                _cancellationToken = new CancellationToken();
                _produtoRepository = new Mock<IProdutoRepository>();
                _imgbbService = new Mock<IImgbbService>();

                _serviceProvider = new Mock<IServiceProvider>();

                _serviceProvider.Setup(s => s.GetService(typeof(IProdutoRepository))).Returns(_produtoRepository.Object);
                _serviceProvider.Setup(s => s.GetService(typeof(IImgbbService))).Returns(_imgbbService.Object);

                _mediator.Setup(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()));
                _mediator.Setup(x => x.PublicarEvento(It.IsAny<Event>()));
                _mediator.Setup(x => x.PublicarDomainEvent(It.IsAny<DomainEvent>()));

                _uow.Setup(x => x.Commit()).Returns(Task.FromResult(true));
            }

            public CatalogoCommandHandler ObterCommandHandler() => new CatalogoCommandHandler(
                produtoRepository: _produtoRepository.Object,
                mediatorHandler: _mediator.Object,
                imgbbService: _imgbbService.Object);
        }
    }
}
