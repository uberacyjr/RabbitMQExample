using System;
using RabbitMQExample.AMQP.Worker_Queue;

namespace RabbitMQExample.ConsoleWorker1
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
