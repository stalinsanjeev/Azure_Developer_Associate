

using CosmosDB;
using Microsoft.Azure.Cosmos;

string cosmosEndpointUri = "https://cossanjeev200.documents.azure.com:443/";
string cosmosDBKey = "eLEo297qubzM9SjvHrDms0Y0eaBysSvsW9zsF4rHu9Ew7U05B35X0R4qcGtHsnNJNfkk1CCQJbvnRIqlEdWtEg==";
string databaseName = "appdb";
string containerName = "Orders";

await ReadItem();

async Task ReadItem()
{
    CosmosClient cosmosClient;
    cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    Database database = cosmosClient.GetDatabase(databaseName);
    Container container = database.GetContainer(containerName);

    string sqlQuery = "SELECT o.orderId,o.category,o.quantity from Orders o";

    QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);

    FeedIterator<Order> feedIterator = container.GetItemQueryIterator<Order>(queryDefinition);

    while(feedIterator.HasMoreResults)
    {
        FeedResponse<Order> feedResponse = await feedIterator.ReadNextAsync();
        foreach(Order order in feedResponse)
        {
            Console.WriteLine("Order is {0}", order.orderId);
            Console.WriteLine("category is {0}", order.category);
            Console.WriteLine("quantity is {0}", order.quantity);

        }
    }

}