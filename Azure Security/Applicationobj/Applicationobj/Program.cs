using Azure.Core;
using Azure.Identity;
using Azure.Storage.Blobs;


string clientId = "a3919973-b800-48aa-8459-0003d53c842a";
string tenantId = "47baacca-57b5-4a92-9c3d-315c72ae653c";


string blobUri = "https://appstore5678.blob.core.windows.net/data/01.sql";
string filePath = "/Users/b.sanjeev/Desktop/";

TokenCredential tokenCredential = new DefaultAzureCredential();



BlobClient blobClient = new BlobClient(new Uri(blobUri), tokenCredential);

await blobClient.DownloadToAsync(filePath);

Console.WriteLine("The blob is downloaded");


