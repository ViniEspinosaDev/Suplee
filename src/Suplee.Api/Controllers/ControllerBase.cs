using MediatR;
using Microsoft.AspNetCore.Mvc;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace Suplee.Catalogo.Api.Controllers
{
    /// <summary>
    /// Controler base
    /// </summary>
    [ApiController]
    public abstract class ControllerBase : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="mediatorHandler"></param>
        protected ControllerBase(INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
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
    }
}
