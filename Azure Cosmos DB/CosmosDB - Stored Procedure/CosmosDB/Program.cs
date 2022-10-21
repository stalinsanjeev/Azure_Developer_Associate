
/*function Display()
{
    var context=getContext();//current context of the request
    var response=context.getResponse();
    
    response.setBody("This is a stored procedure");
}*/

using CosmosDB;
using Microsoft.Azure.Cosmos;

string cosmosEndpointUri = "https://cossanjeev200.documents.azure.com:443/";
string cosmosDBKey = "eLEo297qubzM9SjvHrDms0Y0eaBysSvsW9zsF4rHu9Ew7U05B35X0R4qcGtHsnNJNfkk1CCQJbvnRIqlEdWtEg==";
string databaseName = "appdb";
string containerName = "Orders";

await CallStoredProcedure();

async Task CallStoredProcedure()
{
    CosmosClient cosmosClient;
    cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    
    Container container = cosmosClient.GetContainer(databaseName,containerName);

    var result = await container.Scripts.ExecuteStoredProcedureAsync<string>("Display", new PartitionKey(""), null);

    Console.WriteLine(result);
}