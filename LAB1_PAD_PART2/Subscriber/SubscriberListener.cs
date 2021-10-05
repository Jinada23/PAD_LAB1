using Grpc.Net.Client;
using GrpcMessager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace Subscriber
{
    public class SubscriberListener
    {
        public static async Task Subscribe(IWebHost host)
        {
            var channel = GrpcChannel.ForAddress(Endpoints.BROKER_ENDPOINT);
            var client = new SubscriberProto.SubscriberProtoClient(channel);

            Console.WriteLine("Enter the topic:");
            var address = host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.First();
            var topic = Console.ReadLine();

            var subscribeRequest = new SubscribeRequest { Topic = topic, Address = address };

            try
            {
                var response = await client.SubscribeAsync(subscribeRequest);
            }
            catch (Exception e)
            {
                Console.WriteLine("Data could not been send. " + e.Message);
            }
        }
    }
}
