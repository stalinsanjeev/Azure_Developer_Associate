

using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Newtonsoft.Json;
using StorageQueue;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=storage3453;AccountKey=HJ3HjjMoZe1xLQPBGu4A01OGsw4VVteFpX0v4DRsle/HDQPXzZKz0xdmx/Avo0qzlqTFy2pADM0K+AStrWE67w==;EndpointSuffix=core.windows.net";
string queueName = "appqueue";

sendMessage("01",100);
sendMessage("02",200);




void sendMessage(string orderid,int quantity)
{
    QueueClient queueClient = new QueueClient(connectionString, queueName);

    if (queueClient.Exists())
    {
        Order order = new Order { OrderID = orderid, Quantity = quantity };
        var jsonObject = JsonConvert.SerializeObject(order);
        var bytes = System.Text.Encoding.UTF8.GetBytes(jsonObject);
        var message = System.Convert.ToBase64String(bytes);

        queueClient.SendMessage(message);
        Console.WriteLine("The order information has been sent");
    }
}


void PeekMessage()
{
        QueueClient queueClient = new QueueClient(connectionString, queueName);
        int maxMessages = 10;

        PeekedMessage[] peekMessages = queueClient.PeekMessages(maxMessages);

        Console.WriteLine("The messages in the queue are :");

        foreach(var peekMessage in peekMessages)
        {
            Order order = JsonConvert.DeserializeObject<Order>(peekMessage.Body.ToString());
            
            Console.WriteLine("Order Id {0}",order.OrderID);
            Console.WriteLine("Quantity is {0}", order.Quantity);
        }



 }


void RecieveMessage()
{
    QueueClient queueClient = new QueueClient(connectionString, queueName);
    int maxMessages = 10;

    QueueMessage[] queueMessages = queueClient.ReceiveMessages(maxMessages);

    Console.WriteLine("The messages in the queue are :");

    foreach (var message in queueMessages)
    {
        Console.WriteLine(message.Body);
        queueClient.DeleteMessage(message.MessageId,message.PopReceipt);
    }



}

int GetQueueLength()
{
    QueueClient queueClient = new QueueClient(connectionString, queueName);
    if(queueClient.Exists())
    {
        QueueProperties properties = queueClient.GetProperties();
        return properties.ApproximateMessagesCount;

    }
    return 0;

}

