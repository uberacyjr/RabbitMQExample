using RabbitMQ.Client;

namespace RabbitMQExample.Infrastructure
{
    public static class RabbitMQHelper
    {
        public  static IModel CreateRabbitMqModel()
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
            return model;
        }
    }
}
