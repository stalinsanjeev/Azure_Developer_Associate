using Azure.Messaging.EventHubs.Consumer;
using System.Text;

string connectionString = "Endpoint=sb://appnamespace20030.servicebus.windows.net/;SharedAccessKeyName=ListenPolicy;SharedAccessKey=ANe7AZHuhLHjHGtSo//1IctO1ZzkVp6+SkVsvU+VuRw=;EntityPath=apphub";
string eventHubName = "apphub";
string consumerGroup= "$Default";

//await GetPartitionIds();
await ReadEvents();

async Task GetPartitionIds()
{
    EventHubConsumerClient eventHubConsumerClient = new EventHubConsumerClient(consumerGroup,connectionString);

    string[] partitionIds = await eventHubConsumerClient.GetPartitionIdsAsync();
    foreach(string partitionId in partitionIds)
        Console.WriteLine("Partition Id {0}",partitionId);    
}


async Task ReadEvents()
{
    EventHubConsumerClient eventHubConsumerClient = new EventHubConsumerClient(consumerGroup, connectionString);
    var cancellationSource = new CancellationTokenSource();
    cancellationSource.CancelAfter(TimeSpan.FromSeconds(300));

    await foreach (PartitionEvent _event in eventHubConsumerClient.ReadEventsAsync(cancellationSource.Token))
    {
        Console.WriteLine($"Partition ID {_event.Partition.PartitionId}");
        Console.WriteLine($"Data Offset {_event.Data.Offset}");
        Console.WriteLine($"Sequence Number {_event.Data.SequenceNumber}");
        Console.WriteLine($"Partition Key {_event.Data.PartitionKey}");
        Console.WriteLine(Encoding.UTF8.GetString(_event.Data.EventBody));
    }
}
