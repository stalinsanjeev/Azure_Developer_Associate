

using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using System.Text;

string connectionString = "Endpoint=sb://appnamespace20030.servicebus.windows.net/;SharedAccessKeyName=ListenPolicy;SharedAccessKey=ANe7AZHuhLHjHGtSo//1IctO1ZzkVp6+SkVsvU+VuRw=;EntityPath=apphub";
string consumerGroup = "$Default";
string blobConnectionString = "DefaultEndpointsProtocol=https;AccountName=eventhubstore576656;AccountKey=qBvdTEsMIvwRSvm5brQnsgxIb6gbCJEkTIyDhum6q/6dlidzofotgnW1jr28I+U36rn/hkfZHPdIHAvDAmFnWg==;EndpointSuffix=core.windows.net";
string containerName = "data";

BlobContainerClient blobContainerClient= new BlobContainerClient(blobConnectionString,containerName);
EventProcessorClient eventProcessorClient= new EventProcessorClient(blobContainerClient,consumerGroup,connectionString);
eventProcessorClient.ProcessEventAsync += ProcessEvents;
eventProcessorClient.ProcessErrorAsync += ErrorHandler;

await eventProcessorClient.StartProcessingAsync();
Console.ReadKey();
await eventProcessorClient.StopProcessingAsync();   

async Task ProcessEvents(ProcessEventArgs processEvent)
{
    Console.WriteLine(processEvent.Data.EventBody.ToString());
}

static Task ErrorHandler(ProcessErrorEventArgs errorEvent)
{
    Console.WriteLine(errorEvent.Exception.Message);
    return Task.CompletedTask;
}