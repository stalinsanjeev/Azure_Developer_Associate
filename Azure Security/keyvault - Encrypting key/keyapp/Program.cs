


using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;

string tenantId = "47baacca-57b5-4a92-9c3d-315c72ae653c";
string clientId = "a03e23e1-1ade-4670-947f-8af79a8dffbf";
string clientSecret = "6oX8Q~1lf3N8eDJE-ozqFNMj~5._WSEuOdSsMcvH";


ClientSecretCredential clientSecrectCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

string keyvaultURL = "https://keyvault57.vault.azure.net/";
string keyname = "appkey4";
string texttoEncrypt = "this is a secret text";

KeyClient keyClient = new KeyClient(new Uri(keyvaultURL),clientSecrectCredential);

var key = keyClient.GetKey(keyname);

var cryptoClient = new CryptographyClient(key.Value.Id, clientSecrectCredential);

byte[] textToBytes = Encoding.UTF8.GetBytes(texttoEncrypt);

EncryptResult result = cryptoClient.Encrypt(EncryptionAlgorithm.RsaOaep, textToBytes);

Console.WriteLine("The enxrypted string is ");
Console.WriteLine(Convert.ToBase64String(result.Ciphertext));

