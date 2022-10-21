using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System.Net.Http.Headers;

namespace AuthApp.Pages
{
    
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        public string accessToken;
        private readonly GraphServiceClient _graphServiceClient;
        public string displayName;
        public string userPrincipalName;
        public IndexModel(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }

        public async Task OnGet()
        {
            var user = await _graphServiceClient.Me.Request().GetAsync();
            displayName = user.DisplayName;
            userPrincipalName = user.UserPrincipalName;
        }
    }
}