

using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using ServiceBusQueue;

string connectionString = "Endpoint=sb://appnamespace2000.servicebus.windows.net/;SharedAccessKeyName=appqueuepolicy;SharedAccessKey=/zj/z0OS8Dc2NDZ/NDnWzYN8WGFgVdr6eDDTb99UZxA=;EntityPath=appqueue";
string queueName = "appqueue";

string[] Importance = new string[] { "High", "Medium", "Low" };

List<Order> orders = new List<Order>()
{
    new Order(){OrderID="01",Quantity=100,UnitPrice=9.99F},
    new Order(){OrderID="02",Quantity=200,UnitPrice=10.99F},
    new Order(){OrderID="03",Quantity=300,UnitPrice=8.99F}
};

await SendMessage(orders);

//await PeekMessages();

//await RecieveMessages();

//await GetProperties();

async Task SendMessage(List<Order> orders)
{
    ServiceBusClient serviceBusClient = new ServiceBusClient(connectionString);
    ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(queueName);

    ServiceBusMessageBatch serviceBusMessageBatch = await serviceBusSender.CreateMessageBatchAsync();
    int i = 0;
    foreach (Order order in orders)
    {
        ServiceBusMessage serviceBusMessage = new ServiceBusMessage(JsonConvert.SerializeObject(order));
        serviceBusMessage.ContentType = "application/json";
        serviceBusMessage.ApplicationProperties.Add("Importance", Importance[i]);
        i++;
        if (!serviceBusMessageBatch.TryAddMessage(
               serviceBusMessage))
        {
            throw new Exception("Error occured");
        }
    }
    Console.WriteLine("Sending messages");
    await serviceBusSender.SendMessagesAsync(serviceBusMessageBatch);

    serviceBusClient.DisposeAsync();
    serviceBusSender.DisposeAsync();
}

async Task PeekMessages()
{
    ServiceBusClient serviceBusClient = new ServiceBusClient(connectionString);
    ServiceBusReceiver serviceBusReceiver = serviceBusClient.CreateReceiver(queueName,
        new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock });

    IAsyncEnumerable<ServiceBusReceivedMessage> messages = serviceBusReceiver.ReceiveMessagesAsync();

    await foreach (ServiceBusReceivedMessage message in messages)
    {
        Order order = JsonConvert.DeserializeObject<Order>(message.Body.ToString());
        Console.WriteLine("Order Id {0}", order.OrderID);
        Console.WriteLine("Quantity {0}", order.Quantity);
        Console.WriteLine("Unit Price {0}", order.UnitPrice);

    }
}

async Task RecieveMessages()
{
    ServiceBusClient serviceBusClient = new ServiceBusClient(connectionString);
    ServiceBusReceiver serviceBusReceiver = serviceBusClient.CreateReceiver(queueName,
        new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });

    IAsyncEnumerable<ServiceBusReceivedMessage> messages = serviceBusReceiver.ReceiveMessagesAsync();

    await foreach (ServiceBusReceivedMessage message in messages)
    {
        Order order = JsonConvert.DeserializeObject<Order>(message.Body.ToString());
        Console.WriteLine("Order Id {0}", order.OrderID);
        Console.WriteLine("Quantity {0}", order.Quantity);
        Console.WriteLine("Unit Price {0}", order.UnitPrice);

    }
}

async Task GetProperties()
{
    ServiceBusClient serviceBusClient = new ServiceBusClient(connectionString);
    ServiceBusReceiver serviceBusReceiver = serviceBusClient.CreateReceiver(queueName,
        new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock });

    IAsyncEnumerable<ServiceBusReceivedMessage> messages = serviceBusReceiver.ReceiveMessagesAsync();

    await foreach (ServiceBusReceivedMessage message in messages)
    {
        Console.WriteLine("Message id is {0}",message.MessageId);
        Console.WriteLine("Sequence number is {0}", message.SequenceNumber);
        Console.WriteLine("Message importance is {0}", message.ApplicationProperties["Importance"]);
    }

    serviceBusClient.DisposeAsync();
    serviceBusReceiver.DisposeAsync();
}