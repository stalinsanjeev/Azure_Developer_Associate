
using Azure.Data.Tables;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=tableaccount1003;AccountKey=hdmSoctkma8e8KKuKX3u2uA9hUHbwmGFkrRDX0vAwfZHvWwvrcfVtYGUGIAc6CCdOfRvH8YiLc5qxSGQMi2hsQ==;TableEndpoint=https://tableaccount1003.table.cosmos.azure.com:443/;";
string tablename = "Orders";


AddEntity("01", "Mobile", 100);
AddEntity("02", "Laptop", 200);
AddEntity("03", "Desktop", 300);
AddEntity("04", "Laptop", 400);


void AddEntity(string orderID, string category, int quantity)
{
    TableClient tableClient = new TableClient(connectionString, tablename);

    TableEntity tableEntity = new TableEntity(category, orderID)
    {
        {"quantity",quantity}
    };

    tableClient.AddEntity(tableEntity);
    Console.WriteLine("Added Entity with order id {0}", orderID);

}

