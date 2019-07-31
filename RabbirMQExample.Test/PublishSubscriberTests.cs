
using RabbitMQExample.AMQP.Publish_Subscribe;
using Xunit;

namespace RabbirMQExample.Test
{
    public class PublishSubscriberTests
    {
        [Fact]
        public void Test_publish()
        {
            var ps = new PublishSubscriber();
            ps.PublishMenssages();
        }
    }
}
