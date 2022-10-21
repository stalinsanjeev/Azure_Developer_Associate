

using CosmosDB;
using Microsoft.Azure.Cosmos;

string cosmosEndpointUri = "https://cossanjeev200.documents.azure.com:443/";
string cosmosDBKey = "eLEo297qubzM9SjvHrDms0Y0eaBysSvsW9zsF4rHu9Ew7U05B35X0R4qcGtHsnNJNfkk1CCQJbvnRIqlEdWtEg==";
string databaseName = "appdb";
string containerName = "Orders";

await CreateItem();

async Task CreateItem()
{
    CosmosClient cosmosClient;
    cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    Container container = cosmosClient.GetContainer(databaseName, containerName);

    dynamic orderItem =
        new
        {
            id = Guid.NewGuid().ToString(),
            orderId = "O1",
            category = "Laptop"
        };

    await container.CreateItemAsync(orderItem, null, new ItemRequestOptions { PreTriggers = new List<string> { "validateItem1" } });

    Console.WriteLine("Item has been inserted");

}

// Trigger code

/*function validateItem1()
{
    var context = getContext();
    var request = context.getRequest();
    var item=request.getBody();

    

    if(!("quantity" in item))
    {
        item["quantity"]=0;
    }

    request.setBody(item);
}*/