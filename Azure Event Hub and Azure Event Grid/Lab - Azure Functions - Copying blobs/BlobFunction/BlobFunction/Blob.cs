using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace BlobFunction
{
    public class Blob
    {
        [FunctionName("ProcessBlob")]
        public async Task Run([BlobTrigger("data/{name}", Connection = "connectionString")]Stream streamBlob, [Blob("newdata/{name}", FileAccess.ReadWrite)] BlobClient newBlob, ILogger log)
        {
            
            await newBlob.UploadAsync(streamBlob);
        }
    }
}
