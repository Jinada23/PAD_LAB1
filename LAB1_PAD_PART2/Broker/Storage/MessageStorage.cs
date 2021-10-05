using Broker.Storage.Interfaces;
using System.Collections.Concurrent;
using Utilities.Models;

namespace Broker.Storage
{
    public class MessageStorage : IMessageStorage
    {
        private readonly ConcurrentQueue<MessageData> _messages;

        public MessageStorage()
        {
            _messages = new ConcurrentQueue<MessageData>();
        }

        public void Add(MessageData message)
        {
            _messages.Enqueue(message);
        }

        public MessageData GetNext()
        {
            MessageData message;
            _messages.TryDequeue(out message);
            return message;
        }

        public bool IsEmpty()
        {
            return _messages.IsEmpty;
        }

    }
}
