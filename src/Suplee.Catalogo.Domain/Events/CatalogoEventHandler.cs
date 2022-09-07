using MediatR;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Core.Messages.CommonMessages.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Events
{
    public class CatalogoEventHandler :
        INotificationHandler<ProdutoAdicionadoEvent>,
        INotificationHandler<PedidoIniciadoEvent>
    {
        private readonly IProdutoRepository _produtoLeituraRepository;

        public CatalogoEventHandler(IProdutoRepository produtoRepository)
        {
            _produtoLeituraRepository = produtoRepository;
        }

        public async Task Handle(ProdutoAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            _produtoLeituraRepository.Adicionar(notification.Produto);

            await _produtoLeituraRepository.UnitOfWork.Commit();
        }

        public async Task Handle(PedidoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Debitar estoque (Se der erro lançar evento para tornar pedido rascunho)
        }
    }
}
