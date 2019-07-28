using RabbitMQ.Client;
using System;

namespace RabbitMQExample
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateExchange();
        }

        public static void CreateExchange()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                // "guest"/"guest" by default, limited to localhost connections
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                HostName = "localhost"
            };

            IConnection conn = factory.CreateConnection();
            //AMQP data channel and provides the AMQP operations
            IModel model = conn.CreateModel();
            model.ExchangeDeclare("VSExchange", ExchangeType.Direct);

            model.QueueDeclare("VSQueue", false, false, false, null);
            model.QueueBind("VSQueue", "VSExchange", "", null);
        }
    }
}
