using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Suplee.Catalogo.Api.Controllers.InputModels;
using Suplee.Catalogo.Api.Controllers.ViewModels;
using Suplee.Catalogo.Domain.Commands;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Models;
using Suplee.Catalogo.Domain.ValueObjects;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    public class CatalogoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;

        public CatalogoController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IProdutoRepository produtoRepository,
            IMapper mapper) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obter todas as informações de um produto
        /// </summary>
        /// <param name="produtoId"></param>
        /// <returns></returns>
        [HttpGet("{produtoId}")]
        public async Task<ActionResult> ObterProduto(Guid produtoId)
        {
            if (produtoId == Guid.Empty)
                return BadRequest(new { Success = false, Errors = "Necessário informar o Id do produto" });

            var produto = await _produtoRepository.ObterProduto(produtoId);

            if (produto is null)
                return BadRequest(new { Success = false, Errors = "Não foi possível obter o produto" });

            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);

            return Ok(produtoViewModel);
        }

        /// <summary>
        /// Obter produtos por Efeito
        /// </summary>
        /// <param name="efeitoId"></param>
        /// <param name="pagina"></param>
        /// <param name="quantidade"></param>
        /// <returns></returns>
        [HttpGet("produtos/efeito")]
        public async Task<ActionResult> ObterProdutoPorEfeito(
            [FromQuery] Guid efeitoId,
            [FromQuery] int? pagina,
            [FromQuery] int? quantidade)
        {
            var produtos = await _produtoRepository
                .ObterProdutosPaginadoPorEfeito(efeitoId, pagina ?? 0, quantidade ?? 9);

            if (produtos is null)
                return BadRequest(new { Success = false, Errors = "Não foi possível obter os produtos" });

            var produtosViewModel = _mapper.Map<List<ProdutoResumidoViewModel>>(produtos);

            return Ok(produtosViewModel);
        }

        /// <summary>
        /// Obter produtos por Categoria
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <param name="pagina"></param>
        /// <param name="quantidade"></param>
        /// <returns></returns>
        [HttpGet("produtos/categoria")]
        public async Task<ActionResult> ObterProdutoPorCategoria(
            [FromQuery] Guid categoriaId,
            [FromQuery] int? pagina,
            [FromQuery] int? quantidade)
        {
            var produtos = await _produtoRepository
                .ObterProdutosPaginadoPorCategoria(categoriaId, pagina ?? 0, quantidade ?? 9);

            if (produtos is null)
                return BadRequest(new { Success = false, Errors = "Não foi possível obter os produtos" });

            var produtosViewModel = _mapper.Map<List<ProdutoResumidoViewModel>>(produtos);

            return Ok(produtosViewModel);
        }

        /// <summary>
        /// Obter todos os produtos sem filtro
        /// </summary>
        /// <returns></returns>
        [HttpGet("produtos")]
        public async Task<ActionResult> ObterProdutos(
            [FromQuery] int? pagina,
            [FromQuery] int? quantidade)
        {
            var produtos = await _produtoRepository.ObterProdutosPaginado(pagina ?? 0, quantidade ?? 9);

            if (produtos is null)
                return BadRequest(new { Success = false, Errors = "Não foi possível obter os produtos" });

            var produtosViewModel = _mapper.Map<List<ProdutoResumidoViewModel>>(produtos);

            return Ok(produtosViewModel);
        }

        /// <summary>
        /// Obter todos os efeitos
        /// </summary>
        /// <returns></returns>
        [HttpGet("efeitos")]
        public async Task<ActionResult> ObterEfeitos()
        {
            var efeitos = await _produtoRepository.ObterEfeitos();

            if (efeitos is null)
                return BadRequest(new { Success = false, Errors = "Não foi possível obter os efeitos" });

            return Ok(efeitos);
        }

        /// <summary>
        /// Obter todas as categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet("categorias")]
        public async Task<ActionResult> ObterCategorias()
        {
            var efeitos = await _produtoRepository.ObterCategorias();

            if (efeitos is null)
                return BadRequest(new { Success = false, Errors = "Não foi possível obter as categorias" });

            return Ok(efeitos);
        }

        /// <summary>
        /// Criar produto
        /// </summary>
        /// <param name="produtoInputModel"></param>
        /// <returns></returns>
        [HttpPost("produto")]
        public async Task<ActionResult> CriarProduto(ProdutoInputModel produtoInputModel)
        {
            var informacaoNutricional = _mapper.Map<InformacaoNutricional>(produtoInputModel.InformacaoNutricional);

            informacaoNutricional.MapearCompostosNutricionais();

            var comando = new AdicionarProdutoCommand(
                categoriaId: produtoInputModel.CategoriaId,
                nome: produtoInputModel.Nome,
                descricao: produtoInputModel.Descricao,
                composicao: produtoInputModel.Composicao,
                quantidadeDisponivel: produtoInputModel.QuantidadeDisponivel,
                preco: produtoInputModel.Preco,
                dimensoes: new Dimensoes(produtoInputModel.Profundidade, produtoInputModel.Altura, produtoInputModel.Largura),
                imagens: produtoInputModel.Imagens,
                efeitos: produtoInputModel.Efeitos,
                informacaoNutricional: informacaoNutricional);

            await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return BadRequest(new { Success = false, Errors = ObterMensagensErro() });

            return Ok();
        }
    }
}
