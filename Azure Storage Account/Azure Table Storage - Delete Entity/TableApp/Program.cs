
using Azure;
using Azure.Data.Tables;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=storagesanjeev123;AccountKey=veS1Bj9fghWL9TeGT2JvIWSE+Nt+Ljz+bzejVakm+yHzkWG7+fh8pgZwCkjs/vXa27BTegmZaLDp+ASt4kw2Bw==;EndpointSuffix=core.windows.net";
string tablename = "Orders";


/*AddEntity("01", "Mobile", 100);
AddEntity("02", "Laptop", 200);
AddEntity("03", "Desktop", 300);
AddEntity("04", "Laptop", 400);*/


DeleteEntity("Laptop", "02");


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

void QueryEntity(String category)
{
    TableClient tableClient = new TableClient(connectionString, tablename);

    Pageable<TableEntity> results = tableClient.Query<TableEntity>(entity => entity.PartitionKey == category);

    foreach(TableEntity tableEntity in results)
    {
        Console.Write("Order id is {0}", tableEntity.RowKey);
        Console.Write("Quantity is {0}", tableEntity.GetInt32("quantity"));


    }
}

void DeleteEntity(string category,string OrderID)
{
    TableClient tableClient = new TableClient(connectionString, tablename);
    tableClient.DeleteEntity(category, OrderID);
    Console.WriteLine("Entity is deleted");
}

