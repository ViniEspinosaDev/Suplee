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
using System.Threading.Tasks;

namespace Suplee.Api.Controllers.Vendas
{
    /// <summary>
    /// Endpoints de vendas
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class VendasController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;

        /// <summary>
        /// Construtor de vendas
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="mediatorHandler"></param>
        /// <param name="usuario"></param>
        /// <param name="mapper"></param>
        public VendasController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IUsuarioLogado usuario,
            IMapper mapper) : base(notifications, mediatorHandler, usuario)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        /// <summary>
        /// Cadastrar carrinho com os produtos
        /// </summary>
        /// <param name="cadastrarCarrinho"></param>
        /// <returns></returns>
        [HttpPost("cadastrar-carrinho")]
        public async Task<ActionResult> CadastrarCarrinho(CadastrarCarrinhoInputModel cadastrarCarrinho)
        {
            cadastrarCarrinho.UsuarioId = UsuarioId;

            var comando = _mapper.Map<CadastrarCarrinhoCommand>(cadastrarCarrinho);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        /// <summary>
        /// Inserir produto no carrinho
        /// </summary>
        /// <param name="produtoCarrinho"></param>
        /// <returns></returns>
        [HttpPost("inserir-produto-carrinho")]
        public async Task<ActionResult> InserirProdutoCarrinho(InserirProdutoCarrinhoInputModel produtoCarrinho)
        {
            produtoCarrinho.UsuarioId = UsuarioId;

            var comando = _mapper.Map<InserirProdutoCarrinhoCommand>(produtoCarrinho);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        /// <summary>
        /// Atualizar produto no carrinho
        /// </summary>
        /// <param name="produtoCarrinho"></param>
        /// <returns></returns>
        [HttpPut("atualizar-produto-carrinho")]
        public async Task<ActionResult> AtualizarProdutoCarrinho(AtualizarProdutoCarrinhoInputModel produtoCarrinho)
        {
            produtoCarrinho.UsuarioId = UsuarioId;
            
            var comando = _mapper.Map<AtualizarProdutoCarrinhoCommand>(produtoCarrinho);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        /// <summary>
        /// Excluir produto do carrinho
        /// </summary>
        /// <param name="produtoCarrinho"></param>
        /// <returns></returns>
        [HttpDelete("excluir-produto-carrinho")]
        public async Task<ActionResult> ExcluirProdutoCarrinho(ExcluirProdutoCarrinhoInputModel produtoCarrinho)
        {
            produtoCarrinho.UsuarioId = UsuarioId;

            var comando = _mapper.Map<ExcluirProdutoCarrinhoCommand>(produtoCarrinho);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        /// <summary>
        /// Fazer o pagamento do pedido
        /// </summary>
        /// <param name="realizarPagamento"></param>
        /// <returns></returns>
        [HttpPost("realizar-pagamento")]
        public async Task<ActionResult> RealizarPagamento(RealizarPagamentoInputModel realizarPagamento)
        {
            realizarPagamento.UsuarioId = UsuarioId;

            var comando = _mapper.Map<IniciarPedidoCommand>(realizarPagamento);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        // Recuperar o carrinho

        // Recuperar histórico de pedido
    }
}
