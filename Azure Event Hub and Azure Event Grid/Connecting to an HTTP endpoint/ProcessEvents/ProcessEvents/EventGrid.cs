using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace ProcessEvents
{
    public static class EventGrid
    {
        [FunctionName("ProcessEvents")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // First we need to get the request body
            string request = new StreamReader(req.Body).ReadToEnd();

            log.LogInformation($"Received request body {request}");

            EventGridSubscriber eventGridSubscriber = new EventGridSubscriber();

            EventGridEvent[] eventGridEvents = eventGridSubscriber.DeserializeEventGridEvents(request);

            foreach (EventGridEvent eventGridEvent in eventGridEvents)
            {
                if (eventGridEvent.Data is SubscriptionValidationEventData)
                {
                    SubscriptionValidationEventData validationEventData = (SubscriptionValidationEventData)eventGridEvent.Data;
                    log.LogInformation($"Validation code {validationEventData.ValidationCode}");
                    log.LogInformation($"Validation URL {validationEventData.ValidationUrl}");

                    SubscriptionValidationResponse response = new SubscriptionValidationResponse()
                    {
                        ValidationResponse = validationEventData.ValidationCode
                    };
                    return new OkObjectResult(response);
                }
                else
                {
                    log.LogInformation(eventGridEvent.Data.ToString());
                }
                
            }

            return new OkObjectResult(string.Empty);

        }
    }
}
