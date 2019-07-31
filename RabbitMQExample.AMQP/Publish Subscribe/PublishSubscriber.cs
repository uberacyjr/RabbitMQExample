using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQExample.Infrastructure;
using RabbitMQExample.Model;

namespace RabbitMQExample.AMQP.Publish_Subscribe
{
    public class PublishSubscriber
    {
        private string ExchangeName = "PublishSubscribe_Exchange";

        public void PublishMenssages()
        {
            IModel channel = RabbitMQHelper.CreateRabbitMqModel();
            channel.ExchangeDeclare(ExchangeName, "fanout");

            for (int i = 15; i < 500; i++)
            {
                var payment6 = new Payment { AmountToPay = i, CardNumber = "2945973", Name = "Zé" };
                SendMessage(payment6, channel);
            }

        }
        public void SendMessage(Payment message, IModel channel)
        {
            channel.BasicPublish(ExchangeName, "", null, message.Serialize());
        }

        public void ReceiveLogs()
        {
            IModel channel = RabbitMQHelper.CreateRabbitMqModel();
            channel.ExchangeDeclare(ExchangeName, "fanout");
            string queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: "");
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = (Payment)ea.Body.DeSerialize(typeof(Payment));
                Console.WriteLine(body.AmountToPay);
            };
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            Console.WriteLine(" Press [enter] to exit.");

        }
    }
}
