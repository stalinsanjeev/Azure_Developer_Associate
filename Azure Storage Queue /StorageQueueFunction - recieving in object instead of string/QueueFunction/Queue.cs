using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace QueueFunction
{
    public class Queue
    {
        [FunctionName("GetMessages")]
        public void Run([QueueTrigger("appqueue", Connection = "ConnectionString")]Order order, ILogger log)
        {
            log.LogInformation("Order ID is {0}",order.OrderID);
            log.LogInformation("Qunatity  is {0}", order.Quantity);
        }
    }
}

