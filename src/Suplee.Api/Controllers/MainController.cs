using MediatR;
using Microsoft.AspNetCore.Mvc;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Enums;
using Suplee.Identidade.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Suplee.Catalogo.Api.Controllers
{
    /// <summary>
    /// Controler base
    /// </summary>
    [ApiController]
    public abstract class MainController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        /// <summary>
        /// Informações do usuário logado
        /// </summary>
        protected readonly IUsuario _usuario;

        /// <summary>
        /// Id do usuário logado
        /// </summary>
        protected Guid UsuarioId { get; }

        /// <summary>
        /// Usuário está logado
        /// </summary>
        protected bool UsuarioAutenticado { get; }

        /// <summary>
        /// Usuário logado possuí acesso adminitrador
        /// </summary>
        protected bool AcessoAdministrador { get; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="mediatorHandler"></param>
        /// <param name="usuario"></param>
        protected MainController(INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler mediatorHandler,
                                 IUsuario usuario)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
            _usuario = usuario;

            if (usuario.EstaAutenticado())
            {
                UsuarioId = usuario.UsuarioId;
                UsuarioAutenticado = true;
                AcessoAdministrador = usuario.TipoUsuario == ETipoUsuario.Administrador;
            }
        }

        /// <summary>
        /// Método que retorna se operação foi invalidada
        /// </summary>
        /// <returns></returns>
        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }

        /// <summary>
        /// Metodo que retorna mensagens de erro
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<string> ObterMensagensErro()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        }

        /// <summary>
        /// Metodo que notifica erro
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="mensagem"></param>
        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }

        // TODO: Alterar resposta do método "CustomResponse"
        /// <summary>
        /// Retorna resposta genérica
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult CustomResponse(object result = null)
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }
    }
}
