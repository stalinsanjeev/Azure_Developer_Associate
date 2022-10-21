

using Microsoft.Azure.Cosmos;

string cosmosEndpointUri = "https://cossanjeev200.documents.azure.com:443/";
string cosmosDBKey = "eLEo297qubzM9SjvHrDms0Y0eaBysSvsW9zsF4rHu9Ew7U05B35X0R4qcGtHsnNJNfkk1CCQJbvnRIqlEdWtEg==";

//await CreateDatabase("appdb");

await CreateContainer("appdb", "Orders", "/category");

async Task CreateDatabase(string databaseName)
{
    CosmosClient cosmosClient;
    cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    await cosmosClient.CreateDatabaseAsync(databaseName);
    Console.WriteLine("Database Created");


}

async Task CreateContainer(string databaseName, string containerName, string partitionKey)
{
    CosmosClient cosmosClient;
    cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    Database database = cosmosClient.GetDatabase(databaseName);

    await database.CreateContainerAsync(containerName,partitionKey);

    Console.WriteLine("Container Created");
}