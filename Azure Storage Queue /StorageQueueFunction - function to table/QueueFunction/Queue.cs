using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace QueueFunction
{
    public class Queue
    {
        [FunctionName("GetMessages")]
        [return: Table("Orders",Connection = "ConnectionString")]
        public TableOrder Run([QueueTrigger("appqueue", Connection = "ConnectionString")]Order order, ILogger log)
        {

            TableOrder tableOder = new TableOrder()
            {
                PartitionKey = order.OrderID,
                RowKey = order.Quantity.ToString()
            };
            log.LogInformation("Order Information has been written to the tabel");
            return tableOder;
        }
    }


}

