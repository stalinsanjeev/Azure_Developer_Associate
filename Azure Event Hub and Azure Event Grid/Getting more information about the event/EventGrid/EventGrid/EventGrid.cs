// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;

namespace EventGrid
{`
    public static class EventGrid
    {
        [FunctionName("ProcessEvent")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(eventGridEvent.Id);
            log.LogInformation(eventGridEvent.Subject);
            log.LogInformation(eventGridEvent.Topic);
            log.LogInformation(eventGridEvent.EventType);
            log.LogInformation(eventGridEvent.Data.ToString());
        }
    }
}
