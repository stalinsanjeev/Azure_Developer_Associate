
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
    new Device() { deviceId = "D1",temperature=39.9f},
    new Device() { deviceId = "D2",temperature=36.4f},
    new Device() { deviceId = "D2",temperature=37.4f},
    new Device() { deviceId = "D3",temperature=38.9f},
    new Device() { deviceId = "D4",temperature=35.4f},
};

await SendData(deviceList);

async Task SendData(List<Device> deviceList)
{
    EventHubProducerClient eventHubProducerClient = new EventHubProducerClient(connectionString, eventHubName);
    EventDataBatch eventBatch = await eventHubProducerClient.CreateBatchAsync();

    foreach (Device device in deviceList)
    {
        EventData eventData=new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(device)));
        if (!eventBatch.TryAdd(eventData))
            Console.WriteLine("Error has occured");
    }

    await eventHubProducerClient.SendAsync(eventBatch);
    Console.WriteLine("Events sent");
    await eventHubProducerClient.DisposeAsync();  
}