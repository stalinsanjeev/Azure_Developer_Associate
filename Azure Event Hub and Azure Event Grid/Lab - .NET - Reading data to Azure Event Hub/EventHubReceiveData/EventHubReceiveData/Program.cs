using Azure.Messaging.EventHubs.Consumer;
using System.Text;

string connectionString = "Endpoint=sb://appnamespace10030.servicebus.windows.net/;SharedAccessKeyName=ListenPolicy;SharedAccessKey=l+tND917+d/22KstjWSABY/B25ukdUN6v3OfHeGBKOc=;EntityPath=apphub";
string consumerGroup = "$Default";


//await GetPartitionIds();
await ReadEvents();

async Task GetPartitionIds()
{
    EventHubConsumerClient eventHubConsumerClient= new EventHubConsumerClient(consumerGroup,connectionString);

    string[] partitionIds = await eventHubConsumerClient.GetPartitionIdsAsync();
    foreach(string partitionId in partitionIds)
        Console.WriteLine("Partition Id {0}",partitionId);
}

async Task ReadEvents()
{
    EventHubConsumerClient eventHubConsumerClient = new EventHubConsumerClient(consumerGroup, connectionString);
    var cancellationSource=new CancellationTokenSource();
    cancellationSource.CancelAfter(TimeSpan.FromSeconds(300));

    await foreach(PartitionEvent partitionEvent in eventHubConsumerClient.ReadEventsAsync(cancellationSource.Token))
    {
        Console.WriteLine($"Partition ID {partitionEvent.Partition.PartitionId}");
        Console.WriteLine($"Data Offset {partitionEvent.Data.Offset}");
        Console.WriteLine($"Sequence Number {partitionEvent.Data.SequenceNumber}");
        Console.WriteLine($"Partition Key {partitionEvent.Data.PartitionKey}");
        Console.WriteLine(Encoding.UTF8.GetString(partitionEvent.Data.EventBody));

    }

}
