using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Data;

namespace sqlfunction
{
    public static class AddProduct
    {
        [FunctionName("AddProduct")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Product product = JsonConvert.DeserializeObject<Product>(requestBody);
            SqlConnection conn = GetConnection();

            conn.Open();

            string statement = "INSERT INTO Products(ProductID,ProductName,Quantity) VALUES (@param1,@param2,@param3)";

            using (SqlCommand cmd = new SqlCommand(statement, conn))
            {
                cmd.Parameters.Add("@Param1",System.Data.SqlDbType.Int).Value = product.ProductID;
                cmd.Parameters.Add("@Param2", System.Data.SqlDbType.VarChar).Value = product.ProductName;
                cmd.Parameters.Add("@Param3", System.Data.SqlDbType.Int).Value = product.Quantity;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }


            return new OkObjectResult("PRODUCT HAS BEEN ADDED");

        }


        private static SqlConnection GetConnection()
        {
            string connectionstring = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SQLConnectionString");
            return new SqlConnection(connectionstring);
        }



    }
}

