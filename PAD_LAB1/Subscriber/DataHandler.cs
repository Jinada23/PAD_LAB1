using Newtonsoft.Json;
using System;
using System.Text;
using Utilities;

namespace Subscriber
{
    class DataHandler
    {
        public static void Handle(byte[] data)
        {
            var messageString = Encoding.UTF8.GetString(data);

            var message = JsonConvert.DeserializeObject<MessageData>(messageString);


            switch (message.Topic)
            {
                case "christmas": { Console.WriteLine($"********{message.Message}*********"); break; }
                case "1 june": { Console.WriteLine($"!!!!!!!!!!!{message.Message}!!!!!!!!!"); break; }
                default:
                    { Console.WriteLine(message.Message); }
                    break;
            }
            
        }
    }
}
