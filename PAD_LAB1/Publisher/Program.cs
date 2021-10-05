using Newtonsoft.Json;
using System;
using System.Text;
using Utilities;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {   
            Console.WriteLine("Publisher");
            var publisher  = new PublisherSocket();
            publisher.Connect(Settings.ADDRESS, Settings.PORT);

            if (publisher.isConnected)
            {
                while (true)
                {
                    var messageData = new MessageData();
                    Console.WriteLine("Enter the topic:");
                    messageData.Topic = Console.ReadLine().ToLower();
                    Console.WriteLine("Enter the message:");
                    messageData.Message = Console.ReadLine();
                    
                    var messageString = JsonConvert.SerializeObject(messageData);

                    var data = Encoding.UTF8.GetBytes(messageString);

                    publisher.Send(data);
                }
            }

            Console.ReadLine();

        }
    }
}
