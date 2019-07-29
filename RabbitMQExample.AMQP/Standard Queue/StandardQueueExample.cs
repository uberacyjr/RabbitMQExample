using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQExample.Infrastructure;
using RabbitMQExample.Model;
using System.Diagnostics;

namespace RabbitMQExample.AMQP.Standard_Queue
{
    public class StandardQueueExample
    {
        private  string QueueName { get; }
        public StandardQueueExample(string queueName)
        {
            QueueName = queueName;
        }

        public void CreateStandardQueue()
        {
            var model = RabbitMQHelper.CreateRabbitMqModel();
            model.QueueDeclare(QueueName, true, false, false, null);
        }

        public void SendMessage(Payment message)
        {
            var model = RabbitMQHelper.CreateRabbitMqModel();
            model.BasicPublish("", QueueName, false, null, message.Serialize());
            Debug.WriteLine($"Payment Message Sent: {message.CardNumber}, {message.AmountToPay}");
        }

        public DefaultBasicConsumer Recieve()
        {
            var model = RabbitMQHelper.CreateRabbitMqModel();
            var consumer = new EventingBasicConsumer(model);
            var total = model.MessageCount(QueueName);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.DeSerialize(typeof(Payment));
                // ... process the message
                model.BasicAck(ea.DeliveryTag, false);
            };
            // model.BasicConsume(QueueName, true, consumer);
            string consumerTag = model.BasicConsume(QueueName, false, consumer);
            return consumer;
        }
    }
}
