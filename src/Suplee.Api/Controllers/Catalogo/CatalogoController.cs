using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suplee.Api.Controllers.Catalogo.ViewModels;
using Suplee.Catalogo.Api.Controllers.Catalogo.InputModels;
using Suplee.Catalogo.Domain.Commands;
using Suplee.Catalogo.Domain.DTO;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Interfaces.Repositories;
using Suplee.Catalogo.Domain.Interfaces.Services;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Api.Controllers.Catalogo
{
    [Authorize]
    [Route("api/[controller]")]
    public class CatalogoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoLeituraRepository _produtoLeituraRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICorreiosService _correiosService;
        private readonly IMapper _mapper;

        private const int QuantidadePorPagina = 6;

        public CatalogoController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IUsuarioLogado usuario,
            IProdutoRepository produtoRepository,
            IProdutoLeituraRepository produtoLeituraRepository,
            IMapper mapper,
            ICorreiosService correiosService) : base(notifications, mediatorHandler, usuario)
        {
            _mediatorHandler = mediatorHandler;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _correiosService = correiosService;
            _produtoLeituraRepository = produtoLeituraRepository;
        }

        [AllowAnonymous]
        [HttpGet("{produtoId}")]
        public async Task<ActionResult> ObterProduto(Guid produtoId)
        {
            if (produtoId == Guid.Empty)
            {
                NotificarErro("", "Necessário informar o Id do produto");
                return CustomResponse();
            }

            var produto = await _produtoLeituraRepository.RecuperarProduto(produtoId);

            if (produto is null)
            {
                NotificarErro("", "Não foi possível obter o produto");
                return CustomResponse();
            }

            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);

            return Ok(produtoViewModel);
        }

        [AllowAnonymous]
        [HttpGet("produtos/nome")]
        public async Task<ActionResult> ObterProdutosPeloNome([FromQuery] string nome, [FromQuery] int? pagina, [FromQuery] int? quantidade)
        {
            var produtos = await _produtoLeituraRepository.RecuperarProdutosComEstoquePeloNomeProduto(nome);

            if (produtos is null)
            {
                NotificarErro("", "Não foi possível obter os produtos");
                return CustomResponse();
            }

            var retorno = await MapearRetornoDosMetodosRecuperamProdutoReduzido(produtos, pagina ?? 0, quantidade ?? QuantidadePorPagina);

            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpGet("produtos/id-efeito")]
        public async Task<ActionResult> ObterProdutoPorIdEfeito([FromQuery] Guid efeitoId, [FromQuery] int? pagina, [FromQuery] int? quantidade)
        {
            var produtos = await _produtoLeituraRepository.RecuperarProdutosComEstoquePeloIdEfeito(efeitoId);

            if (produtos is null)
            {
                NotificarErro("", "Não foi possível obter os produtos");
                return CustomResponse();
            }

            var retorno = await MapearRetornoDosMetodosRecuperamProdutoReduzido(produtos, pagina ?? 0, quantidade ?? QuantidadePorPagina);

            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpGet("produtos/nome-efeito")]
        public async Task<ActionResult> ObterProdutoPorNomeEfeito([FromQuery] string nomeEfeito, [FromQuery] int? pagina, [FromQuery] int? quantidade)
        {
            var produtos = await _produtoLeituraRepository.RecuperarProdutosComEstoquePeloNomeEfeito(nomeEfeito.Trim());

            if (produtos is null)
            {
                NotificarErro("", "Não foi possível obter os produtos");
                return CustomResponse();
            }

            var retorno = await MapearRetornoDosMetodosRecuperamProdutoReduzido(produtos, pagina ?? 0, quantidade ?? QuantidadePorPagina);

            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpGet("produtos/id-categoria")]
        public async Task<ActionResult> ObterProdutoPorIdCategoria([FromQuery] Guid categoriaId, [FromQuery] int? pagina, [FromQuery] int? quantidade)
        {
            var produtos = await _produtoLeituraRepository.RecuperarProdutosComEstoquePeloIdCategoria(categoriaId);

            if (produtos is null)
            {
                NotificarErro("", "Não foi possível obter os produtos");
                return CustomResponse();
            }

            var retorno = await MapearRetornoDosMetodosRecuperamProdutoReduzido(produtos, pagina ?? 0, quantidade ?? QuantidadePorPagina);

            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpGet("produtos/nome-categoria")]
        public async Task<ActionResult> ObterProdutoPorNomeCategoria([FromQuery] string nomeCategoria, [FromQuery] int? pagina, [FromQuery] int? quantidade)
        {
            var produtos = await _produtoLeituraRepository.RecuperarProdutosComEstoquePeloNomeCategoria(nomeCategoria);

            if (produtos is null)
            {
                NotificarErro("", "Não foi possível obter os produtos");
                return CustomResponse();
            }

            var retorno = await MapearRetornoDosMetodosRecuperamProdutoReduzido(produtos, pagina ?? 0, quantidade ?? QuantidadePorPagina);

            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpGet("produtos")]
        public async Task<ActionResult> ObterProdutos([FromQuery] int? pagina, [FromQuery] int? quantidade)
        {
            var produtos = await _produtoLeituraRepository.RecuperarProdutosComEstoque();

            if (produtos is null)
            {
                NotificarErro("", "Não foi possível obter os produtos");
                return CustomResponse();
            }

            var retorno = await MapearRetornoDosMetodosRecuperamProdutoReduzido(produtos, pagina ?? 0, quantidade ?? QuantidadePorPagina);

            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpGet("efeitos")]
        public async Task<ActionResult> ObterEfeitos()
        {
            var efeitos = await _produtoRepository.ObterEfeitos();

            if (efeitos is null)
            {
                NotificarErro("", "Não foi possível obter os efeitos");
                return CustomResponse();
            }

            return Ok(efeitos);
        }

        [AllowAnonymous]
        [HttpGet("categorias")]
        public async Task<ActionResult> ObterCategorias()
        {
            var efeitos = await _produtoRepository.ObterCategorias();

            if (efeitos is null)
            {
                NotificarErro("", "Não foi possível obter as categorias");
                return CustomResponse();
            }

            return Ok(efeitos);
        }

        [AllowAnonymous]
        [HttpGet("preco-prazo/{produtoId}/{cep}")]
        public ActionResult ObterPrecoPrazo(Guid produtoId, string cep)
        {
            var frete = _mapper.Map<FreteViewModel>(_correiosService.CalcularFrete(produtoId, cep));

            return CustomResponse(frete);
        }

        [AllowAnonymous]
        [HttpPost("produto")]
        public async Task<ActionResult> CriarProduto([FromForm] ProdutoInputModel produtoInputModel)
        {
            var comando = _mapper.Map<AdicionarProdutoCommand>(produtoInputModel);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        private async Task<object> MapearRetornoDosMetodosRecuperamProdutoReduzido(List<ProdutoDTO> produtos, int paginas, int quantidades)
        {
            int quantidadePeloFiltro = produtos.Count();

            if (paginas > 0)
                produtos = produtos.Skip((paginas - 1) * quantidades).ToList();

            if (quantidades > 0)
                produtos = produtos.Take(quantidades).ToList();

            int quantidadeTotalProdutos = await _produtoLeituraRepository.QuantidadeTotalProdutos();
            int quantidadeProdutosComEstoque = await _produtoLeituraRepository.QuantidadeProdutosComEstoque();

            var produtosViewModel = _mapper.Map<List<ProdutoResumidoViewModel>>(produtos);

            var retorno = new
            {
                quantidadeTotalProdutos = quantidadeTotalProdutos,
                quantidadeTotalProdutosComEstoque = quantidadeProdutosComEstoque,
                quantidadeProdutosPeloFiltro = quantidadePeloFiltro,
                quantidadeAtual = produtos.Count(),
                produtos = produtosViewModel
            };

            return retorno;
        }
    }
}
