
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using EventHubSendData;
using Newtonsoft.Json;
using System.Text;

string connectionString = "Endpoint=sb://appnamespace20030.servicebus.windows.net/;SharedAccessKeyName=SendPolicy;SharedAccessKey=die7lZbfDKxLfxiAKSReQz70lkckxh6xD/eL/Gq1lQQ=;EntityPath=apphub";
string eventHubName = "apphub";

List<Device> deviceList = new List<Device>()
{
    new Device() { deviceId = "D1",temperature=40.0f},
    new Device() { deviceId = "D1",temperature=39.9f}    
};

await SendData(deviceList);

async Task SendData(List<Device> deviceList)
{
    EventHubProducerClient eventHubProducerClient = new EventHubProducerClient(connectionString, eventHubName);
    List<EventData> eventData = new List<EventData>();

    foreach (Device device in deviceList)
    {
        eventData.Add(new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(device))));
    }
    
    await eventHubProducerClient.SendAsync(eventData, new SendEventOptions() { PartitionKey = "D1" },
        new System.Threading.CancellationToken());
    
    Console.WriteLine("Events sent");
    await eventHubProducerClient.DisposeAsync();  
}