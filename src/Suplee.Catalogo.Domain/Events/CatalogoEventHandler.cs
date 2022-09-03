using MediatR;
using Suplee.Catalogo.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Events
{
    public class CatalogoEventHandler :
        INotificationHandler<ProdutoAdicionadoEvent>
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
    }
}
