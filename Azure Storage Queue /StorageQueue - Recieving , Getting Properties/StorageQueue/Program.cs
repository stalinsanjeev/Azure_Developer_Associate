

using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=storage3453;AccountKey=HJ3HjjMoZe1xLQPBGu4A01OGsw4VVteFpX0v4DRsle/HDQPXzZKz0xdmx/Avo0qzlqTFy2pADM0K+AStrWE67w==;EndpointSuffix=core.windows.net";
string queueName = "appqueue";

sendMessage("Test Message 1");
sendMessage("Test Message 2");

Console.WriteLine("The number of messages in the queue are :{0}", GetQueueLength());


//PeekMessage();

//RecieveMessage();

void sendMessage(string Message)
{
    QueueClient queueClient = new QueueClient(connectionString, queueName);

    if (queueClient.Exists())
    {
        queueClient.SendMessage(Message);
        Console.WriteLine("The message has been sent");
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
            Console.WriteLine(peekMessage.Body);
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

