using System.Collections.Concurrent;
using Utilities;

namespace Broker
{
    public class MessageStorage
    {
        private static ConcurrentQueue<MessageData> _messagesQueue;

        static MessageStorage()
        {
            _messagesQueue = new ConcurrentQueue<MessageData>();
        }

        public static void Add(MessageData message)
        {
            _messagesQueue.Enqueue(message);
        }

        public static MessageData GetNext()
        {
            MessageData message;

            _messagesQueue.TryDequeue(out message);

            return message;
        }

        public static bool IsEmpty()
        {
            return _messagesQueue.IsEmpty;
        }
    }
}
