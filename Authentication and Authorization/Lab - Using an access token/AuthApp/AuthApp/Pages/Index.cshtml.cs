using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace AuthApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        public string blobContent;

        public IndexModel(ILogger<IndexModel> logger, ITokenAcquisition tokenAcquisition)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;

        }

        public async Task OnGet()
        {
            TokenAcquisitionTokenCredential tokenAcquisitionTokenCredential = new TokenAcquisitionTokenCredential(_tokenAcquisition);

            Uri blobUri = new Uri("https://appstore50505.blob.core.windows.net/data/script.sql");
            BlobClient blobClient = new BlobClient(blobUri, tokenAcquisitionTokenCredential);

            MemoryStream ms = new MemoryStream();
            blobClient.DownloadTo(ms);
            ms.Position = 0;

            StreamReader sr = new StreamReader(ms);
            blobContent = sr.ReadToEnd();

        }
    }
}