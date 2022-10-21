

using CosmosDB;
using Microsoft.Azure.Cosmos;

string cosmosEndpointUri = "https://cossanjeev200.documents.azure.com:443/";
string cosmosDBKey = "eLEo297qubzM9SjvHrDms0Y0eaBysSvsW9zsF4rHu9Ew7U05B35X0R4qcGtHsnNJNfkk1CCQJbvnRIqlEdWtEg==";
string databaseName = "appdb";
string containerName = "Orders";

await AddItem("01", "Laptop", 100);
await AddItem("02", "Mobiles", 200);
await AddItem("03", "Desktop", 75);
await AddItem("04", "Laptop", 25);


async Task AddItem(string orderId , string category , int quantity)
{
    CosmosClient cosmosClient;
    cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    Database database = cosmosClient.GetDatabase(databaseName);
    Container container = database.GetContainer(containerName);

    Order order = new Order()
    {
        id = Guid.NewGuid().ToString(),
        orderId = orderId,
        category = category,
        quantity = quantity
    };

    ItemResponse<Order> response = await container.CreateItemAsync<Order>(order, new PartitionKey(category));

    Console.WriteLine("Added item with Order Id {0}", orderId);
    Console.WriteLine("Request Units {0}", response.RequestCharge);

}