using Microsoft.AspNetCore.ResponseCompression;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

//add redis connection

builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("BlazeZen.redis.cache.windows.net:6380,password=hTQj8wUJqYQo2mXLicItFt55qtvATTF8EAzCaMAKC1A=,ssl=True,abortConnect=False"));

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

                    