
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using static System.Net.WebRequestMethods;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=storagesanjeev123;AccountKey=veS1Bj9fghWL9TeGT2JvIWSE+Nt+Ljz+bzejVakm+yHzkWG7+fh8pgZwCkjs/vXa27BTegmZaLDp+ASt4kw2Bw==;EndpointSuffix=core.windows.net";
string containerName = "scripts";
string blobName = "01.sql";
string filePath = "/Users/b.sanjeev/Desktop/01.sql";


await AcquireLease();

async Task AcquireLease()
{

    string blobName = "01.sql";
    BlobClient blobClient = new BlobClient(connectionString, containerName, blobName);

    BlobLeaseClient blobLeaseClient = blobClient.GetBlobLeaseClient();
    TimeSpan leaseTime = new TimeSpan(0, 0, 1, 00);

    Response<BlobLease> response = await blobLeaseClient.AcquireAsync(leaseTime);


    Console.WriteLine("Lease id is {0}", response.Value.LeaseId);

}




