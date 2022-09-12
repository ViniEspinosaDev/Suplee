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
        /// <param name="carrinhoInputModel"></param>
        /// <returns></returns>
        [HttpPost("cadastrar-carrinho")]
        public async Task<ActionResult> CadastrarCarrinho(CadastrarCarrinhoInputModel carrinhoInputModel)
        {
            carrinhoInputModel.UsuarioId = UsuarioId;

            var comando = _mapper.Map<CadastrarCarrinhoCommand>(carrinhoInputModel);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse();
        }

        // Recuperar o carrinho

        // Recuperar histórico de pedido

        // Atualizar produto no carrinho

        // Excluir produto do carrinho

        // Inserir produto no carrinho

        // Fazer o pagamento do pedido
    }
}
