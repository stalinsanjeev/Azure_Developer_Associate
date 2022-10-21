using System;
namespace ServiceBusQueue
{
    public class Order
    {
        public string OrderID { get; set; }

        public int Quantity { get; set; }

        public float UnitPrice { get; set; }
    }
}

