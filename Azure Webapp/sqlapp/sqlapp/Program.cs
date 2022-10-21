using Microsoft.FeatureManagement;
using sqlapp.Services;

var builder = WebApplication.CreateBuilder(args);


var connectionString = "Endpoint=https://appconfig45.azconfig.io;Id=cZZm-l8-s0:xTsOFgsMaaE4OSp1mYBN;Secret=5rW+eNiZZl39SfiSZMzLYfZJ0w3Llp7IV/H6hSo0BGc=";



builder.Host.ConfigureAppConfiguration(builder =>
{
    builder.AddAzureAppConfiguration(options =>
    
        options.Connect(connectionString).UseFeatureFlags());
   
}
);

builder.Services.AddTransient<IProductService, ProductService>();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddFeatureManagement();

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
