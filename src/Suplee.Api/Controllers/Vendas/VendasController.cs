using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suplee.Api.Controllers.Vendas.InputModels;
using Suplee.Api.Controllers.Vendas.ViewModels;
using Suplee.Catalogo.Api.Controllers;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Vendas.Domain.Commands;
using Suplee.Vendas.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Suplee.Api.Controllers.Vendas
{
    [Authorize]
    [Route("api/[controller]")]
    public class VendasController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPedidoRepository _pedidoRepository;

        public VendasController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IUsuarioLogado usuario,
            IMapper mapper,
            IPedidoRepository pedidoRepository) : base(notifications, mediatorHandler, usuario)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
        }

        [HttpPost("cadastrar-carrinho")]
        public async Task<ActionResult> CadastrarCarrinho(CadastrarCarrinhoInputModel cadastrarCarrinho)
        {
            var comando = _mapper.Map<CadastrarCarrinhoCommand>(cadastrarCarrinho);

            comando.VincularUsuarioId(UsuarioId);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        [HttpPost("inserir-produto-carrinho")]
        public async Task<ActionResult> InserirProdutoCarrinho(InserirProdutoCarrinhoInputModel produtoCarrinho)
        {
            var comando = _mapper.Map<InserirProdutoCarrinhoCommand>(produtoCarrinho);

            comando.VincularUsuarioId(UsuarioId);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        [HttpPut("atualizar-produto-carrinho")]
        public async Task<ActionResult> AtualizarProdutoCarrinho(AtualizarProdutoCarrinhoInputModel produtoCarrinho)
        {
            var comando = _mapper.Map<AtualizarProdutoCarrinhoCommand>(produtoCarrinho);

            comando.VincularUsuarioId(UsuarioId);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        [HttpDelete("excluir-produto-carrinho")]
        public async Task<ActionResult> ExcluirProdutoCarrinho(ExcluirProdutoCarrinhoInputModel produtoCarrinho)
        {
            var comando = _mapper.Map<ExcluirProdutoCarrinhoCommand>(produtoCarrinho);

            comando.VincularUsuarioId(UsuarioId);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        [HttpPost("realizar-pagamento")]
        public async Task<ActionResult> RealizarPagamento(RealizarPagamentoInputModel realizarPagamento)
        {
            var comando = _mapper.Map<IniciarPedidoCommand>(realizarPagamento);

            comando.VincularUsuarioId(UsuarioId);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        [HttpGet("recuperar-carrinho")]
        public async Task<ActionResult> RecuperarCarrinho()
        {
            var pedidoRascunho = await _pedidoRepository.ObterCarrinhoPorUsuarioId(UsuarioId);

            var carrinhoViewModel = _mapper.Map<PedidoViewModel>(pedidoRascunho);

            return CustomResponse(carrinhoViewModel);
        }

        [HttpGet("recuperar-historico")]
        public async Task<ActionResult> RecuperarHistoricoCompras()
        {
            var pedidos = await _pedidoRepository.ObterHistoricoPedido(UsuarioId);

            if (pedidos == null || pedidos.Count == 0)
                return CustomResponse(null);

            var historicoViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);

            return CustomResponse(historicoViewModel);
        }
    }
}
