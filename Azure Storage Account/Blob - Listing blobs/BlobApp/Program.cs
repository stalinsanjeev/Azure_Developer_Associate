
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using static System.Net.WebRequestMethods;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=storagesanjeev123;AccountKey=veS1Bj9fghWL9TeGT2JvIWSE+Nt+Ljz+bzejVakm+yHzkWG7+fh8pgZwCkjs/vXa27BTegmZaLDp+ASt4kw2Bw==;EndpointSuffix=core.windows.net";
string containerName = "data";
string blobName = "01.sql";
string filePath = "/Users/b.sanjeev/Desktop/01.sql";


BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);

await foreach (BlobItem blobItem in blobContainerClient.GetBlobsAsync())
{
    Console.WriteLine("The Blob Name is {0}", blobItem.Name);
    Console.WriteLine("The Blob Size is {0}", blobItem.Properties.ContentLength);
}


