

using CosmosDB;
using Microsoft.Azure.Cosmos;

string cosmosDBEndpointUri = "https://cossanjeev200.documents.azure.com:443/";
string cosmosDBKey = "eLEo297qubzM9SjvHrDms0Y0eaBysSvsW9zsF4rHu9Ew7U05B35X0R4qcGtHsnNJNfkk1CCQJbvnRIqlEdWtEg==";
string databaseName = "appdb";
string containerName = "Orders";

await DeleteItem();




async Task DeleteItem()
{
    CosmosClient cosmosClient;
    cosmosClient = new CosmosClient(cosmosDBEndpointUri, cosmosDBKey);

    Database database = cosmosClient.GetDatabase(databaseName);
    Container container = database.GetContainer(containerName);

    string orderId = "O1";
    string sqlQuery = $"SELECT o.id,o.category FROM Orders o WHERE o.orderId='{orderId}'";

    string id = "";
    string category = "";

    QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
    using FeedIterator<Order> feedIterator = container.GetItemQueryIterator<Order>(queryDefinition);

    while (feedIterator.HasMoreResults)
    {
        FeedResponse<Order> respose = await feedIterator.ReadNextAsync();
        foreach (Order order in respose)
        {
            id = order.id;
            category = order.category;
        }
    }



    ItemResponse<Order> orderResponse = await container.DeleteItemAsync<Order>(id, new PartitionKey(category));


    Console.WriteLine("Item is updated");

}
