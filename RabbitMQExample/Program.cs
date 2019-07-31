using System;
using RabbitMQExample.AMQP.Worker_Queue;

namespace RabbitMQExample.ConsoleWorker0
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkerQueue.CreateTaskQueue();
            Console.ReadLine();
        }
    }
}
