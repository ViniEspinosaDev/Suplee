﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Suplee.Catalogo.Api.Controllers.InputModels;
using Suplee.Catalogo.Domain.Commands;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Models;
using Suplee.Catalogo.Domain.ValueObjects;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
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

        [HttpGet]
        public async Task<ActionResult> ObterProdutos()
        {
            var produtos = await _produtoRepository.ObterProdutos();

            if (produtos is null)
                return BadRequest();

            return Ok(produtos);
        }

        [HttpGet("efeitos")]
        public async Task<ActionResult> ObterEfeitos()
        {
            var efeitos = await _produtoRepository.ObterEfeitos();

            if (efeitos is null)
                return BadRequest();

            return Ok(efeitos);
        }

        [HttpGet("categorias")]
        public async Task<ActionResult> ObterCategorias()
        {
            var efeitos = await _produtoRepository.ObterCategorias();

            if (efeitos is null)
                return BadRequest();

            return Ok(efeitos);
        }

        [HttpPost("produto")]
        public async Task<ActionResult> CriarProduto(ProdutoInputModel produtoInputModel)
        {
            var informacaoNutricional = _mapper.Map<InformacaoNutricional>(produtoInputModel.InformacaoNutricional);

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

            if (OperacaoValida())
                return BadRequest(new { Success = false, Errors = ObterMensagensErro() });

            return Ok();
        }
    }
}
