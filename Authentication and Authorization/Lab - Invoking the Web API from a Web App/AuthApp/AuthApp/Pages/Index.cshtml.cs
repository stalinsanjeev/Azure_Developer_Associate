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
            string[] scope = new string[] { "api://1c390173-b289-4873-9cb5-d0b1d1f7f3d6/Product.Read" };
            string accessToken= await _tokenAcquisition.GetAccessTokenForUserAsync(scope);


            string apiURL = "https://productapi1000.azurewebsites.net/api/Product";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage responseMessage = await client.GetAsync(apiURL);
            content = await responseMessage.Content.ReadAsStringAsync();



        }
    }
}