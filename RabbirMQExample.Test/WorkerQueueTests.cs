using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQExample.AMQP.Standard_Queue;
using RabbitMQExample.Infrastructure;
using RabbitMQExample.Model;
using Xunit;
using Xunit.Abstractions;

namespace RabbitMQExample.Test
{
    public class WorkerQueueTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private string QueueName = "WorkerQueue_Queue";

        public WorkerQueueTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Create_Worker_Queue()
        {
            var _sq = new StandardQueueExample(QueueName);
            for (int i = 15; i < 50; i++)
            {
                var payment6 = new Payment {AmountToPay = i, CardNumber = "2945973", Name = "Zé"};
                _sq.SendMessage(payment6);
            }

            _sq.CreateStandardQueue();
        }


        [Fact]
        public void Create_Consumer_Worker_Queue()
        {
            IModel channel = RabbitMQHelper.CreateRabbitMqModel();
            channel.QueueDeclare(QueueName, true, false, false, null);

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicQos(0, 1, false);
            uint total = channel.MessageCount(QueueName);
           
            consumer.Received += (ch, ea) =>
            {
                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(QueueName, false, consumer);
        }
    }
}
