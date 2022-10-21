

using Microsoft.Identity.Client;
using System.Net.Http.Headers;

IConfidentialClientApplication confidentialClientApplication;

string clientId = "c2bc5ffd-e08b-49a5-b6ac-cba23e3bc4b7";
string clientSecret = "eze8Q~MQe1nigzX~uEZQVO~ofxo-YXt8YLKgfb1N";
string tenantId = "70c0f6d9-7f3b-4425-a6b6-09b47643ec58";

confidentialClientApplication = ConfidentialClientApplicationBuilder.Create(clientId)
    .WithTenantId(tenantId)
    .WithClientSecret(clientSecret)
    .Build();

string[] scopes = new string[] { "api://1c390173-b289-4873-9cb5-d0b1d1f7f3d6/.default" };

AuthenticationResult result=await confidentialClientApplication.AcquireTokenForClient(scopes).ExecuteAsync();

string accessToken = result.AccessToken;

string apiUrl = "https://productapi1000.azurewebsites.net/api/Product";

HttpClient client = new HttpClient();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

HttpResponseMessage responseMessage = await client.GetAsync(apiUrl);
string content=await responseMessage.Content.ReadAsStringAsync();

Console.WriteLine(content);