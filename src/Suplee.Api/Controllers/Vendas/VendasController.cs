using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suplee.Api.Controllers.Vendas.InputModels;
using Suplee.Catalogo.Api.Controllers;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Vendas.Domain.Commands;
using Suplee.Vendas.Domain.Interfaces;
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
            cadastrarCarrinho.UsuarioId = UsuarioId;

            var comando = _mapper.Map<CadastrarCarrinhoCommand>(cadastrarCarrinho);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        [HttpPost("inserir-produto-carrinho")]
        public async Task<ActionResult> InserirProdutoCarrinho(InserirProdutoCarrinhoInputModel produtoCarrinho)
        {
            produtoCarrinho.UsuarioId = UsuarioId;

            var comando = _mapper.Map<InserirProdutoCarrinhoCommand>(produtoCarrinho);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        [HttpPut("atualizar-produto-carrinho")]
        public async Task<ActionResult> AtualizarProdutoCarrinho(AtualizarProdutoCarrinhoInputModel produtoCarrinho)
        {
            produtoCarrinho.UsuarioId = UsuarioId;

            var comando = _mapper.Map<AtualizarProdutoCarrinhoCommand>(produtoCarrinho);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        [HttpDelete("excluir-produto-carrinho")]
        public async Task<ActionResult> ExcluirProdutoCarrinho(ExcluirProdutoCarrinhoInputModel produtoCarrinho)
        {
            produtoCarrinho.UsuarioId = UsuarioId;

            var comando = _mapper.Map<ExcluirProdutoCarrinhoCommand>(produtoCarrinho);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        [HttpPost("realizar-pagamento")]
        public async Task<ActionResult> RealizarPagamento(RealizarPagamentoInputModel realizarPagamento)
        {
            realizarPagamento.UsuarioId = UsuarioId;

            var comando = _mapper.Map<IniciarPedidoCommand>(realizarPagamento);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        // Recuperar o carrinho
        //[HttpGet("recuperar-carrinho")]
        //public async Task<ActionResult> RecuperarCarrinho()
        //{
        //    var pedidoRascunho = await _pedidoRepository.ObterCarrinhoPorUsuarioId(UsuarioId);


        //}

        // Recuperar histórico de pedido
    }
}
