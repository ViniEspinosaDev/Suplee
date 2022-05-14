using EventStore.ClientAPI;

namespace Suplee.EventSourcing
{
    public interface IEventStoreService
    {
        IEventStoreConnection GetConnection();
    }
}
