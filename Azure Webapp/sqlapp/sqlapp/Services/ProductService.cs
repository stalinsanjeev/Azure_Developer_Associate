using System;
using System.Text.Json;
using Microsoft.FeatureManagement;
using MySql.Data.MySqlClient;
using sqlapp.models;

namespace sqlapp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        private readonly IFeatureManager _featureManger;

        public ProductService(IConfiguration configuration, IFeatureManager featureManager)
        {
            _configuration = configuration;
            _featureManger = featureManager;

        }

        public async Task<bool> IsBeta()
        {
            if(await _featureManger.IsEnabledAsync("beta"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public MySqlConnection GetConection()
        {

            string connectionstring = "Server=mysql; Port=3306; Database=appdb; Uid=root; Pwd=Sanjeev@1234; SslMode=Preferred;";
            return new MySqlConnection(connectionstring);

            // below for using azure functions
            // return new SqlConnection(_configuration["SQLConnection"]);

        }


        public /*Task < */ List<Product> /*>*/ GetProdcuts()
        {
            MySqlConnection conn = GetConection();

            List<Product> _prodcuts_lst = new List<Product>();

            string Statement = "SELECT ProductID,ProductName,Quantity from Products";

            conn.Open();

            MySqlCommand cmd = new MySqlCommand(Statement, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)

                    };

                    _prodcuts_lst.Add(product);
                }

            }
            conn.Close();

            return _prodcuts_lst;

            //The below is using azure functions above is using normal way


            /*String FunctionURL = "https://fnapp34.azurewebsites.net/api/GetProducts?code=uiB57N8YlvR1HqRUWGueRijdmIRIYt9-hDK-TlgcMJ-qAzFuwj1GLg==";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(FunctionURL);

                string content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Product>>(content);

            }*/
        }
    }
}

