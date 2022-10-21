
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using static System.Net.WebRequestMethods;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=storagesanjeev123;AccountKey=veS1Bj9fghWL9TeGT2JvIWSE+Nt+Ljz+bzejVakm+yHzkWG7+fh8pgZwCkjs/vXa27BTegmZaLDp+ASt4kw2Bw==;EndpointSuffix=core.windows.net";
string containerName = "scripts";
string blobName = "01.sql";
string filePath = "/Users/b.sanjeev/Desktop/01.sql";


await setBlobMetaData();
await getMetaData();

async Task setBlobMetaData()
{
    string blobName = "01.sql";
    BlobClient blobClient = new BlobClient(connectionString, containerName, blobName );

    IDictionary<string, string> metaData = new Dictionary<string, string>();
    metaData.Add("Department", "Logistics");
    metaData.Add("Application", "AppA");

    await blobClient.SetMetadataAsync(metaData);

    Console.Write("Metadata Added");

}

async Task getMetaData()
{
    string blobName = "01.sql";
    BlobClient blobClient = new BlobClient(connectionString, containerName, blobName);

    BlobProperties blobProperties = await blobClient.GetPropertiesAsync();


    foreach( var metaData in blobProperties.Metadata)
    {
        Console.Write("The key is {0} the value is {1}", metaData.Key, metaData.Value);
    }

}






