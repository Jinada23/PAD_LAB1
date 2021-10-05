using System;
using Utilities;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Subscriber");
            string topic;

            Console.Write("Enter the topic:");
            topic = Console.ReadLine().ToLower();

            var subscriber = new SubscriberSocket(topic);
            subscriber.Connect(Settings.ADDRESS, Settings.PORT);


            Console.WriteLine("Press any key to exit..");
            Console.ReadLine();
        }
    }
}
