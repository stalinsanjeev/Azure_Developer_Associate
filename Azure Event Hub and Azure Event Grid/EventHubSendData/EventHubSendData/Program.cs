

using Azure.Messaging.EventHubs;
using System.Text;
using Azure.Messaging.EventHubs.Producer;
using EventHubSendData;
using Newtonsoft.Json;

string connectionString = "Endpoint=sb://appnamespaceev2003.servicebus.windows.net/;SharedAccessKeyName=SendPolicy;SharedAccessKey=x3428QfMWxv6CCyu+Dci/6hg0hmCBCISWwj4N1GTBaI=;EntityPath=apphub";
string eventhubName = "apphub";

List <Device> deviceList = new List<Device>()
{
    new Device() { deviceId = "D1", temperature = 40.0f },
    new Device() { deviceId = "D1", temperature = 39.9f },
    new Device() { deviceId = "D2", temperature = 36.4f },
    new Device() { deviceId = "D2", temperature = 37.4f },
    new Device() { deviceId = "D3", temperature = 38.9f },
    new Device() { deviceId = "D4", temperature = 35.4f },
};

await SendData();

async Task SendData()
{
    EventHubProducerClient eventHubProducerClient = new EventHubProducerClient(connectionString,eventhubName);

    EventDataBatch eventDataBatch = await eventHubProducerClient.CreateBatchAsync();

    foreach (Device device in deviceList)
    {
        EventData eventData = new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(device)));
        if (!eventDataBatch.TryAdd(eventData))
            Console.WriteLine("Error has occured");
    }

    await eventHubProducerClient.SendAsync(eventDataBatch);
    Console.WriteLine("Events sent");
    await eventHubProducerClient.DisposeAsync();


}