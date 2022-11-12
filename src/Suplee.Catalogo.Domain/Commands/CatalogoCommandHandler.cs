using MediatR;
using Suplee.Catalogo.Domain.Events;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Interfaces.Services;
using Suplee.Catalogo.Domain.Models;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Commands
{
    public class CatalogoCommandHandler : CommandHandler,
        IRequestHandler<AdicionarProdutoCommand, bool>,
        IRequestHandler<AtualizarProdutoCommand, bool>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IImagemService _imagemService;

        public CatalogoCommandHandler(
            IProdutoRepository produtoRepository,
            IMediatorHandler mediatorHandler,
            IImagemService imagemService) : base(mediatorHandler)
        {
            _produtoRepository = produtoRepository;
            _mediatorHandler = mediatorHandler;
            _imagemService = imagemService;
        }

        public async Task<bool> Handle(AdicionarProdutoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return false;
            }

            request.InformacaoNutricional.MapearCompostosNutricionais();

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

            request.Efeitos.ForEach(x => produto.AdicionarProdutoEfeito(new ProdutoEfeito(produto.Id, x)));

            foreach (var imagem in request.Imagens)
            {
                var produtoImagem = await _imagemService.UploadImagem(imagem);

                if (produtoImagem is null) return false;

                produto.AdicionarProdutoImagem(produtoImagem);
            }

            _produtoRepository.Adicionar(produto);

            var sucesso = await _produtoRepository.UnitOfWork.Commit();

            if (sucesso)
            {
                await _mediatorHandler.PublicarDomainEvent(new ProdutoAdicionadoEvent(produto, request.CategoriaId, request.Efeitos));
            }

            return sucesso;
        }

        public Task<bool> Handle(AtualizarProdutoCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
