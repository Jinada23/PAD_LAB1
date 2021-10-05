using Utilities.Models;

namespace Broker.Storage.Interfaces
{
    public interface IMessageStorage
    {
        void Add(MessageData message);
        MessageData GetNext();
        bool IsEmpty();
    }
}
