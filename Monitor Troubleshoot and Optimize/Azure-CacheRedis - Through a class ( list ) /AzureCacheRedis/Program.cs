

using AzureCacheRedis;
using Newtonsoft.Json;
using StackExchange.Redis;

string connectionString = "appcache100.redis.cache.windows.net:6380,password=DLpma4B79ne8T18QTVsih7iSJWLDrpywqAzCaBjOFbQ=,ssl=True,abortConnect=False";

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);

//GetCacheData();
//SetCacheData("u1",10,100);
//SetCacheData("u1",20, 200);
//SetCacheData("u1",30, 300);

GetCacheDatau("u1");
void SetCacheData(string userid , int prodcutid , int quantity )
{
    IDatabase database = redis.GetDatabase();
    CartItem cartItem = new CartItem { ProductID = prodcutid, Quantity = quantity };


    string key = String.Concat(userid, ":cartitems");
    database.ListRightPush(key,JsonConvert.SerializeObject(cartItem));
    Console.WriteLine("Cache data Has been set");
}

void GetCacheData()
{
    IDatabase database = redis.GetDatabase();
    if (database.KeyExists("top:3:courses"))
        Console.WriteLine(database.StringGet("top:3:courses"));
    else
        Console.WriteLine("The key does not exists");
}




void GetCacheDatau(string userId)
{
    string key = String.Concat(userId, ":cartitems");
    IDatabase database = redis.GetDatabase();
    if (database.KeyExists(key))
    {
        long listLength = database.ListLength(key);
        Console.WriteLine("The number of values are {0}", listLength);
        for (int i = 0; i < listLength; i++)
        {
            string value = database.ListGetByIndex(key, i);
            CartItem cartItem = JsonConvert.DeserializeObject<CartItem>(value);
            Console.WriteLine("Product ID {0}", cartItem.ProductID);
            Console.WriteLine("Quantity {0}", cartItem.Quantity);
        }
    }
    else
        Console.WriteLine("key does not exist");
}