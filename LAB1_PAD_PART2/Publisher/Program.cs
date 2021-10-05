using GrpcMessager;
using System;
using Grpc.Net.Client;
using Utilities;
using System.Threading.Tasks;

namespace Publisher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Publisher");
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(Endpoints.BROKER_ENDPOINT);
            var client = new PublisherProto.PublisherProtoClient(channel);
            
            while (true)
            {
                Console.WriteLine("Enter the topic: ");
                var topic = Console.ReadLine();
                Console.WriteLine("Enter the message: ");
                var message = Console.ReadLine();


                var publishRequest = new PublishRequest
                {
                    Topic = topic,
                    Message = message
                };

                try
                {
                    await client.PublishAsync(publishRequest);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Data could not been send. " + e.Message);
                }
            }

        }
    }
}
