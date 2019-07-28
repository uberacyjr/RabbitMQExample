using RabbitMQExample.Model;
using System;
using Xunit;

namespace RabbitMQExample.Test
{
    public class ObjectSerializeTests
    {

        [Fact]
        public void When_Pass_Object_Return_Serialized_Bytes()
        {
            Payment payment1 = new Payment
            {
                AmountToPay = 25.0m,
                CardNumber = "123456789123"
            };

            byte [] serialized =  payment1.Serialize();

            Assert.NotNull(serialized);
        }
        [Fact]
        public void When_Pass_Serialized_Object_Return_DeSerialized_Object()
        {
            Payment payment1 = new Payment
            {
                AmountToPay = 25.0m,
                CardNumber = "123456789123"
            };

            byte[] serialized = payment1.Serialize();

            var payment_deserialied = serialized.DeSerialize(Type.GetType("object"));

            Assert.NotNull(payment_deserialied);
        }
    }
}
