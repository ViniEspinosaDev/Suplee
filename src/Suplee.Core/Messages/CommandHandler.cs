using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Suplee.Core.Messages
{
    public class CommandHandler
    {
        private readonly IMediatorHandler _mediator;

        public CommandHandler(
            IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        protected async Task NotificarErro(Message message, string errorMessage) =>
            await _mediator.PublicarNotificacao(new DomainNotification(message.MessageType, errorMessage));

        protected void NotificarErros(Message message, IEnumerable<string> errorMessages) =>
            errorMessages.ToList().ForEach(async errorMessage => await _mediator.PublicarNotificacao(new DomainNotification(message.MessageType, errorMessage)));

        protected void NotificarErrosValidacao<R>(Command<R> command)
        {
            foreach (var error in command.ValidationResult.Errors)
            {
                _mediator.PublicarNotificacao(new DomainNotification(command.MessageType, error.ErrorMessage));
            }
        }
    }
}
