using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace sqlfunction
{
    public static class GetProduct
    {
        [FunctionName("GetProducts")]
        public static async Task<IActionResult> RunProducts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            List<Product> _prodcuts_lst = new List<Product>();

            string Statement = "SELECT ProductID,ProductName,Quantity from Products";
            SqlConnection conn = GetConnection();

            conn.Open();

            SqlCommand cmd = new SqlCommand(Statement, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
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

            return new OkObjectResult(JsonConvert.SerializeObject(_prodcuts_lst));


        }






        private static SqlConnection GetConnection()
        {
            string connectionstring = "Server=tcp:appserver14.database.windows.net,1433;Initial Catalog=SQLDatabase;Persist Security Info=False;User ID=sam;Password=Sanjeev@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return new SqlConnection(connectionstring);
        }


        [FunctionName("GetProduct")]
        public static async Task<IActionResult> RunProduct(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            int productId = int.Parse(req.Query["id"]);


            string Statement = String.Format("SELECT ProductID,ProductName,Quantity from Products WHERE ProductID={0}",productId);
            SqlConnection conn = GetConnection();


  


            conn.Open();


            SqlCommand cmd = new SqlCommand(Statement, conn);
            Product product = new Product();



            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    product.ProductID = reader.GetInt32(0);
                    product.ProductName = reader.GetString(1);
                    product.Quantity = reader.GetInt32(2);
                    var response = product;
                    conn.Close();
                    return new OkObjectResult(response);
                    

                }
            }
            catch(Exception e)
            {
                var response = "No records found";
                conn.Close();
                return new OkObjectResult(response);
                
            }

           

        }
    }
}

