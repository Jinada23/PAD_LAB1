using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using Utilities;

namespace Broker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(Endpoints.BROKER_ENDPOINT)
                .Build()
                .Run();
        }
    }
}
