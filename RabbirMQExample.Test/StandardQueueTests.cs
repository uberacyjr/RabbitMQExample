using RabbitMQExample.AMQP.Standard_Queue;
using RabbitMQExample.Model;
using Xunit;

namespace RabbitMQExample.Test
{
    public class StandardQueueTests
    {
        private string QueueName = "StandardQueue_ExampleQueue";
        [Fact]
        public void Create_Standard_Queue()
        {
            var payment1 = new Payment { AmountToPay = 10, CardNumber = "2948484", Name = "Bera" };
            var payment2 = new Payment { AmountToPay = 20, CardNumber = "8848474", Name = "João" };
            var _sq = new StandardQueueExample(QueueName);
            _sq.CreateStandardQueue();
            _sq.SendMessage(payment1);
            _sq.SendMessage(payment2);

            var recieve = _sq.Recieve();
        }
    }
}
