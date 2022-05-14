using MediatR;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Models;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages;
using Suplee.Core.Messages.CommonMessages.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Commands
{
    public class CatalogoCommandHandler : IRequestHandler<AdicionarProdutoCommand, bool>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public CatalogoCommandHandler(IProdutoRepository produtoRepository, IMediatorHandler mediatorHandler)
        {
            _produtoRepository = produtoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AdicionarProdutoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            var produto = new Produto(
                informacaoNutricionalId: request.InformacaoNutricional.Id,
                categoriaId: request.CategoriaId,
                nome: request.Nome,
                descricao: request.Descricao,
                composicao: request.Composicao,
                quantidadeDisponivel: request.QuantidadeDisponivel,
                preco: request.Preco,
                dimensoes: request.Dimensoes);

            produto.AdicionarInformacaoNutricional(request.InformacaoNutricional);

            request.Imagens.ForEach(x => produto.AdicionarProdutoImagem(new ProdutoImagem(produto.Id, x)));
            request.Efeitos.ForEach(x => produto.AdicionarProdutoEfeito(new ProdutoEfeito(produto.Id, x)));

            _produtoRepository.Adicionar(produto);

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private bool ValidarComando(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
