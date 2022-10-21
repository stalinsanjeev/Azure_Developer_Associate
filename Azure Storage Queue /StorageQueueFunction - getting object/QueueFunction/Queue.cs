using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace QueueFunction
{
    public class Queue
    {
        [FunctionName("GetMessages")]
        public void Run([QueueTrigger("appqueue", Connection = "ConnectionString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}

