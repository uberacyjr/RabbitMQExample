using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQExample.Infrastructure;

namespace RabbitMQExample.AMQP.Worker_Queue
{
    public static class WorkerQueue
    {
        private static string QueueName = "WorkerQueue_Queue";

        public static void CreateTaskQueue()
        {
            IModel channel = RabbitMQHelper.CreateRabbitMqModel();
            channel.QueueDeclare(QueueName, true, false, false, null);
            var consumer = new EventingBasicConsumer(channel);

            //Fair dispatch
            //You might have noticed that the dispatching still doesn't work exactly as we want.
            //For example in a situation with two workers, when all odd messages are heavy and even messages are light,
            //one worker will be constantly busy and the other one will do hardly any work. Well,
            //RabbitMQ doesn't know anything about that and will still dispatch messages evenly.
            //this happens because RabbitMQ just dispatches a message when the message enters the queue.
            //It doesn't look at the number of unacknowledged messages for a consumer.
            //It just blindly dispatches every n-th message to the n-th consumer.
            //In order to change this behavior we can use the ***BasicQos*** method with the ***prefetchCount = 1*** setting.
            //This tells RabbitMQ not to give more than one message to a worker at a time. Or, in other words,
            //don't dispatch a new message to a worker until it has processed and acknowledged the previous one.
            //Instead, it will dispatch it to the next worker that is not still busy.
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
