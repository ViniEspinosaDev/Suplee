using Suplee.Core.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Suplee.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId);
    }
}
