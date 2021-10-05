using System;
using System.Threading;
using Utilities;

namespace Broker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Broker");
            var socket = new BrokerSocket();

            socket.Start(Settings.ADDRESS, Settings.PORT);

            var worker = new MessageWorker();
            var thread = new Thread(new ThreadStart(worker.DoWork));
            thread.Start();
            Console.ReadLine();
        }
    }
}
