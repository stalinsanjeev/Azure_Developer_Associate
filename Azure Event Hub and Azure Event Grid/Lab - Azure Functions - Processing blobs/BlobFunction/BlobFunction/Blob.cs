using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace BlobFunction
{
    public class Blob
    {
        [FunctionName("ProcessBlob")]
        public void Run([BlobTrigger("data/{name}", Connection = "connectionString")]Stream streamBlob, string name, ILogger log)
        {
            
            StreamReader streamReader=new StreamReader(streamBlob);
            string blob = streamReader.ReadToEnd();
            log.LogInformation(blob);
        }
    }
}
