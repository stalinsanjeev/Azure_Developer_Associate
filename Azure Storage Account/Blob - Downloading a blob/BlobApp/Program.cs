
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using static System.Net.WebRequestMethods;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=storagesanjeev123;AccountKey=veS1Bj9fghWL9TeGT2JvIWSE+Nt+Ljz+bzejVakm+yHzkWG7+fh8pgZwCkjs/vXa27BTegmZaLDp+ASt4kw2Bw==;EndpointSuffix=core.windows.net";
string containerName = "data";
string blobName = "01.sql";
string filePath = "/Users/b.sanjeev/Desktop/01.sql";


BlobClient blobClient = new BlobClient(connectionString, containerName, blobName);

await blobClient.DownloadToAsync(filePath);

Console.WriteLine("The blob is downloaded");




