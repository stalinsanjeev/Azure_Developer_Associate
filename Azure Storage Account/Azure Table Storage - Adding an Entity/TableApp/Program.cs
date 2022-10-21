
using Azure.Data.Tables;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=storagesanjeev123;AccountKey=veS1Bj9fghWL9TeGT2JvIWSE+Nt+Ljz+bzejVakm+yHzkWG7+fh8pgZwCkjs/vXa27BTegmZaLDp+ASt4kw2Bw==;EndpointSuffix=core.windows.net";
string tablename = "Orders";


AddEntity("01", "Mobile", 100);
AddEntity("02", "Laptop", 200);
AddEntity("03", "Desktop", 300);
AddEntity("04", "Laptop", 400);


void AddEntity(string orderID, string category,int quantity)
{
    TableClient tableClient = new TableClient(connectionString,tablename);

    TableEntity tableEntity = new TableEntity(category, orderID)
    {
        {"quantity",quantity}
    };

    tableClient.AddEntity(tableEntity);
    Console.WriteLine("Added Entity with order id {0}",orderID);

}

