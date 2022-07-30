using Suplee.Core.Messages;
using Suplee.Core.Messages.CommonMessages.DomainEvents;
using Suplee.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace Suplee.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<R> EnviarComando<R>(Command<R> comando);
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
        Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent;
    }
}
