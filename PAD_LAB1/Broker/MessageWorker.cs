using Newtonsoft.Json;
using System.Text;
using System.Threading;

namespace Broker
{
    public class MessageWorker
    {
        public void DoWork()
        {
            while (true)
            {
                while (!MessageStorage.IsEmpty())
                {
                    var message = MessageStorage.GetNext();
                    if (message != null)
                    {
                        var subscribers = ConnectionStorage.GetConnectionsByTopic(message.Topic);

                        foreach (var subscriber in subscribers)
                        {
                            var messageString = JsonConvert.SerializeObject(message);
                            var data = Encoding.UTF8.GetBytes(messageString);
                            subscriber.Socket.Send(data);
                        }

                        if (subscribers.Count == 0)
                        {
                            MessageStorage.Add(message);
                        }
                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
