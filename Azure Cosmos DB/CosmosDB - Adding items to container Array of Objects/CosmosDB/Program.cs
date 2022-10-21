using CosmosDB;
using Microsoft.Azure.Cosmos;

string cosmosEndpointUri = "https://cossanjeev200.documents.azure.com:443/";
string cosmosDBKey = "4miwZ9uLNtrRpntE2D3xCrCssLdaq1EpDtS3dq86bCshOznrC7X0RK7w3VY6R70ru5PfpsZF3qqiw7sJabPi9g==";
string databaseName = "appdb";
string containerName = "Customers";

//await ReadItem();

await AddItem("C1", "CustomerA", "New York",
    new List<Order>()
    {
        new Order
        {
            orderId = "01",
            category = "Laptop",
            quantity = 100
        },
        new Order
        {
            orderId = "03",
            category = "Desktop",
            quantity = 75
        }
    }
    );
    



async Task AddItem(string customerId, string customerName, string customerCity,List<Order> orders)
{
    CosmosClient cosmosClient;
    cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    Database database = cosmosClient.GetDatabase(databaseName);
    Container container = database.GetContainer(containerName);

    Cusotmer customer = new Cusotmer()
    {
        customerId = customerId,
        customerName = customerName,
        customerCity = customerCity,
        orders = orders

    };

    await container.CreateItemAsync<Cusotmer>(customer, new PartitionKey(customerCity));

    Console.WriteLine("Added customer with id {0}", customerId);


}
