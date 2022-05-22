using MediatR;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Models;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.ExternalService.Imgbb.DTO;
using Suplee.ExternalService.Imgbb.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Commands
{
    public class CatalogoCommandHandler :
        IRequestHandler<AdicionarProdutoCommand, bool>,
        IRequestHandler<AtualizarProdutoCommand, bool>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IImgbbService _imgbbService;

        public CatalogoCommandHandler(
            IProdutoRepository produtoRepository,
            IMediatorHandler mediatorHandler,
            IImgbbService imgbbService)
        {
            _produtoRepository = produtoRepository;
            _mediatorHandler = mediatorHandler;
            _imgbbService = imgbbService;
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

            request.Efeitos.ForEach(x => produto.AdicionarProdutoEfeito(new ProdutoEfeito(produto.Id, x)));

            int contador = 1;

            foreach (var imagem in request.Imagens)
            {
                string nomeImagem = $"{produto.Nome.Split(' ').First()}_{contador++}";

                var retorno = await _imgbbService.UploadImage(new ImgbbUploadInputModel(imagem, nomeImagem));

                produto.AdicionarProdutoImagem(new ProdutoImagem(produto.Id, nomeImagem, retorno.Data.data.Url));
            }

            _produtoRepository.Adicionar(produto);

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public Task<bool> Handle(AtualizarProdutoCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
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
