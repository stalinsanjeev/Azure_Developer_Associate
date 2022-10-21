

using CosmosDB;
using Microsoft.Azure.Cosmos;

string cosmosEndpointUri = "https://cossanjeev200.documents.azure.com:443/";
string cosmosDBKey = "eLEo297qubzM9SjvHrDms0Y0eaBysSvsW9zsF4rHu9Ew7U05B35X0R4qcGtHsnNJNfkk1CCQJbvnRIqlEdWtEg==";
string databaseName = "appdb";
string containerName = "Orders";

await CallStoredProcedure();


// Stored Procedure code written in azure stored procedure

/*function createItems(items)
{
    var context = getContext();
    var response = context.getResponse();

    if (!items)
    {
        response.setBody("Error : items are undefined");
        return;
    }

    var numberOfItems = items.length;
    checkLength(numberOfItems);

    for (let i = 0; i < numberOfItems; i++)
    {
        createItem(items[i]);
    }

    function checkLength(itemLength)
    {
        if (itemLength == 0)
        {
            response.setBody("Error : There are no items");
            return;
        }
    }

    function createItem(item)
    {
        var collection = context.getCollection();
        var collectionLink = collection.getSelfLink();
        collection.createDocument(collectionLink, item);
    }
}*/

async Task CallStoredProcedure()
{
    CosmosClient cosmosClient;
    cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    
    Container container = cosmosClient.GetContainer(databaseName,containerName);

    dynamic[] orderItems = new dynamic[]
    {
        new
        {
            id = Guid.NewGuid().ToString(),
            orderId = "01",
            category = "Laptop",
            quantity = 100

        },
        new
        {
            id = Guid.NewGuid().ToString(),
            orderId = "02",
            category = "Laptop",
            quantity = 200

        },
        new
        {
            id = Guid.NewGuid().ToString(),
            orderId = "03",
            category = "Laptop",
            quantity = 75

        }
    };

    var result = await container.Scripts.ExecuteStoredProcedureAsync<string>("createitems", new PartitionKey("Laptop"), new[] {orderItems});

    Console.WriteLine(result);
}


