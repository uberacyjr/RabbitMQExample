using RabbitMQExample.AMQP.Publish_Subscribe;
using System;

namespace RabbitMQExample.ConsolePublishSubscribe
{
    class Program
    {
        static void Main(string[] args)
        {
            var ps = new PublishSubscriber();
            ps.ReceiveLogs();
            Console.ReadLine();
        }
    }
}
