using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suplee.Api.Controllers.Catalogo.ViewModels;
using Suplee.Catalogo.Api.Controllers.Catalogo.InputModels;
using Suplee.Catalogo.Domain.Commands;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Interfaces.Repositories;
using Suplee.Catalogo.Domain.Interfaces.Services;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Interfaces;
using System;
using System.Collections.Generic;
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

        private const int QuantidadePorPagina = 12;

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
        [HttpGet("produtos/nome")]
        public async Task<ActionResult> ObterProdutosPeloNome([FromQuery] string nome, [FromQuery] int? pagina, [FromQuery] int? quantidade)
        {
            var produtos = await _produtoRepository.ObterProdutosPaginadoPorNomeProduto(nome, pagina ?? 0, quantidade ?? QuantidadePorPagina);

            if (produtos is null)
            {
                NotificarErro("", "Não foi possível obter os produtos");
                return CustomResponse();
            }

            int quantidadeProdutos = _produtoRepository.QuantidadeTotalProdutos();
            int quantidadeProdutosPeloNome = _produtoRepository.QuantidadeProdutosPeloNome(nome);

            var produtosViewModel = _mapper.Map<List<ProdutoResumidoViewModel>>(produtos);

            var retorno = new
            {
                quantidadeTotalProdutos = quantidadeProdutos,
                quantidadeProdutosPeloFiltro = quantidadeProdutosPeloNome,
                produtos = produtosViewModel
            };

            return Ok(retorno);
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

            var produto = await _produtoRepository
                .ObterProduto(produtoId);

            if (produto is null)
            {
                NotificarErro("", "Não foi possível obter o produto");
                return CustomResponse();
            }

            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);

            return Ok(produtoViewModel);
        }

        [AllowAnonymous]
        [HttpGet("produtos/id-efeito")]
        public async Task<ActionResult> ObterProdutoPorIdEfeito([FromQuery] Guid efeitoId, [FromQuery] int? pagina, [FromQuery] int? quantidade)
        {
            var produtos = await _produtoRepository
                .ObterProdutosPaginadoPorIdEfeito(efeitoId, pagina ?? 0, quantidade ?? QuantidadePorPagina);

            if (produtos is null)
            {
                NotificarErro("", "Não foi possível obter os produtos");
                return CustomResponse();
            }

            int quantidadeProdutos = _produtoRepository.QuantidadeTotalProdutos();
            int quantidadeProdutosPeloIdEfeito = _produtoRepository.QuantidadeProdutosPeloIdEfeito(efeitoId);

            var produtosViewModel = _mapper.Map<List<ProdutoResumidoViewModel>>(produtos);

            var retorno = new
            {
                quantidadeTotalProdutos = quantidadeProdutos,
                quantidadeProdutosPeloFiltro = quantidadeProdutosPeloIdEfeito,
                produtos = produtosViewModel
            };

            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpGet("produtos/nome-efeito")]
        public async Task<ActionResult> ObterProdutoPorNomeEfeito([FromQuery] string nomeEfeito, [FromQuery] int? pagina, [FromQuery] int? quantidade)
        {
            var produtos = await _produtoRepository
                .ObterProdutosPaginadoPorNomeEfeito(nomeEfeito, pagina ?? 0, quantidade ?? QuantidadePorPagina);

            if (produtos is null)
            {
                NotificarErro("", "Não foi possível obter os produtos");
                return CustomResponse();
            }

            int quantidadeProdutos = _produtoRepository.QuantidadeTotalProdutos();
            int quantidadeProdutosPeloNomeEfeito = _produtoRepository.QuantidadeProdutosPeloNomeEfeito(nomeEfeito);

            var produtosViewModel = _mapper.Map<List<ProdutoResumidoViewModel>>(produtos);

            var retorno = new
            {
                quantidadeTotalProdutos = quantidadeProdutos,
                quantidadeProdutosPeloFiltro = quantidadeProdutosPeloNomeEfeito,
                produtos = produtosViewModel
            };

            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpGet("produtos/id-categoria")]
        public async Task<ActionResult> ObterProdutoPorIdCategoria([FromQuery] Guid categoriaId, [FromQuery] int? pagina, [FromQuery] int? quantidade)
        {
            var produtos = await _produtoRepository
                .ObterProdutosPaginadoPorIdCategoria(categoriaId, pagina ?? 0, quantidade ?? QuantidadePorPagina);

            if (produtos is null)
            {
                NotificarErro("", "Não foi possível obter os produtos");
                return CustomResponse();
            }

            int quantidadeProdutos = _produtoRepository.QuantidadeTotalProdutos();
            int quantidadeProdutosPeloIdCategoria = _produtoRepository.QuantidadeProdutosPeloIdCategoria(categoriaId);

            var produtosViewModel = _mapper.Map<List<ProdutoResumidoViewModel>>(produtos);

            var retorno = new
            {
                quantidadeTotalProdutos = quantidadeProdutos,
                quantidadeProdutosPeloFiltro = quantidadeProdutosPeloIdCategoria,
                produtos = produtosViewModel
            };

            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpGet("produtos/nome-categoria")]
        public async Task<ActionResult> ObterProdutoPorNomeCategoria([FromQuery] string nomeCategoria, [FromQuery] int? pagina, [FromQuery] int? quantidade)
        {
            var produtos = await _produtoRepository
                .ObterProdutosPaginadoPorNomeCategoria(nomeCategoria, pagina ?? 0, quantidade ?? QuantidadePorPagina);

            if (produtos is null)
            {
                NotificarErro("", "Não foi possível obter os produtos");
                return CustomResponse();
            }

            int quantidadeProdutos = _produtoRepository.QuantidadeTotalProdutos();
            int quantidadeProdutosPeloNomeCategoria = _produtoRepository.QuantidadeProdutosPeloNomeCategoria(nomeCategoria);

            var produtosViewModel = _mapper.Map<List<ProdutoResumidoViewModel>>(produtos);

            var retorno = new
            {
                quantidadeTotalProdutos = quantidadeProdutos,
                quantidadeProdutosPeloFiltro = quantidadeProdutosPeloNomeCategoria,
                produtos = produtosViewModel
            };

            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpGet("produtos")]
        public async Task<ActionResult> ObterProdutos([FromQuery] int? pagina, [FromQuery] int? quantidade)
        {
            var produtos = await _produtoRepository
                .ObterProdutosPaginado(pagina ?? 0, quantidade ?? QuantidadePorPagina);

            if (produtos is null)
            {
                NotificarErro("", "Não foi possível obter os produtos");
                return CustomResponse();
            }

            int quantidadeProdutos = _produtoRepository.QuantidadeTotalProdutos();

            var produtosViewModel = _mapper.Map<List<ProdutoResumidoViewModel>>(produtos);

            var retorno = new
            {
                quantidadeTotalProdutos = quantidadeProdutos,
                quantidadeProdutosPeloFiltro = quantidadeProdutos,
                produtos = produtosViewModel
            };

            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpGet("produtos-mongodb")]
        public async Task<ActionResult> ObterProdutosMongoDb()
        {
            var produtos = _produtoLeituraRepository.RecuperarProdutosComEstoque();

            return await Task.FromResult(CustomResponse(produtos));
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
    }
}
