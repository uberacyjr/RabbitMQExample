using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQExample.Model
{
    [Serializable]
    public class PurchaseOrder
    {
        public decimal AmountPay { get; set; }
        public string Number { get; set; }
        public string CompanyName { get; set; }
        public int PaymentDayTerms { get; set; }
    }
}
