using sqlapp.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string connectionString = "appcache100.redis.cache.windows.net:6380,password=DLpma4B79ne8T18QTVsih7iSJWLDrpywqAzCaBjOFbQ=,ssl=True,abortConnect=False";
var multiplexer = ConnectionMultiplexer.Connect(connectionString);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
