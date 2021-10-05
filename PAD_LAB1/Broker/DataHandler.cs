using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using Utilities;

namespace Broker
{
    public class DataHandler
    {
        public static void Handle(byte[] data, ConnectionInfo connection)
        {

            string messageString = Encoding.UTF8.GetString(data);
            
            if (messageString.StartsWith(Settings.SUBSCRIBER_DELIMITATOR))
            {
                connection.Topic = messageString.Split(Settings.SUBSCRIBER_DELIMITATOR).LastOrDefault();
                ConnectionStorage.Add(connection);
            }
            else
            {
                Console.WriteLine(messageString);
                MessageData messageData = JsonConvert.DeserializeObject<MessageData>(messageString);
                MessageStorage.Add(messageData);
            }
        }
    }
}

