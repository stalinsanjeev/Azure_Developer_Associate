using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;
using System.Net.Http.Headers;

namespace AuthApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        public string content;

        public IndexModel(ILogger<IndexModel> logger, ITokenAcquisition tokenAcquisition)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;

        }

        public async Task OnGet()
        {
            string[] scope = new string[] { "api://e676de88-2343-4879-aabe-2e3162812a00/Products" };
            string accessToken= await _tokenAcquisition.GetAccessTokenForUserAsync(scope);


            string apiURL = "https://appmanagement100020.azure-api.net/api/products";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            HttpResponseMessage responseMessage = await client.GetAsync(apiURL);
            content = await responseMessage.Content.ReadAsStringAsync();



        }
    }
}