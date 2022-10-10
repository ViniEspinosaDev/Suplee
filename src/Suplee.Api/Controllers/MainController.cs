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
    [ApiController]
    public abstract class MainController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        protected readonly IUsuarioLogado _usuario;

        protected Guid UsuarioId { get; }
        protected bool UsuarioAutenticado { get; }
        protected bool AcessoAdministrador { get; }

        protected MainController(INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler mediatorHandler,
                                 IUsuarioLogado usuario)
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

        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }

        protected IEnumerable<string> ObterMensagensErro()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                data = ObterMensagensErro()
            });
        }
    }
}
