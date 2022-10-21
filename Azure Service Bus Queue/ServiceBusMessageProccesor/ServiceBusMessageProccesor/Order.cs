using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureServiceBusMessages
{
    public class Order
    {
        public string OrderID { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
    }
}
