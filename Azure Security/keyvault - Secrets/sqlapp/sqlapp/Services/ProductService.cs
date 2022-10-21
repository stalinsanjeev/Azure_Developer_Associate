using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{

    // This service will interact with our Product data in the SQL database
    public class ProductService
    {
        

        private SqlConnection GetConnection()
        {
            string tenantId = "70c0f6d9-7f3b-4425-a6b6-09b47643ec58";
            string clientId = "b4d0b1b0-21f6-4b57-a6cc-ca982114e340";
            string clientSecret = "1ym8Q~uaRr2d5LtGSB9K36JxhJzN-MB2iMxirbyr";

            string keyvaultUrl = "https://appvault600909.vault.azure.net/";
            string secretName = "dbconnectionstring";
            
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            SecretClient secretClient = new SecretClient(new Uri(keyvaultUrl), clientSecretCredential);

            var secret = secretClient.GetSecret(secretName);

            string connectionString = secret.Value.Value;


            return new SqlConnection(connectionString);
        }
        public List<Product> GetProducts()
        {
            List<Product> _product_lst = new List<Product>();
            string _statement = "SELECT ProductID,ProductName,Quantity from Products";
            SqlConnection _connection = GetConnection();
            
            _connection.Open();
            
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            
            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = _reader.GetInt32(0),
                        ProductName = _reader.GetString(1),
                        Quantity = _reader.GetInt32(2)
                    };

                    _product_lst.Add(_product);
                }
            }
            _connection.Close();
            return _product_lst;
        }

    }
}

