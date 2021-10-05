using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading.Tasks;
using Utilities;

namespace Subscriber
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                .UseUrls(Endpoints.SUBSCRIBER_ENDPOINT)
                .UseStartup<Startup>()
                .Build();

            host.Start();

            await SubscriberListener.Subscribe(host);

            Console.WriteLine("Press any key to exit..");
            Console.ReadLine();
        }
    }
}
