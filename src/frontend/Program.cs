using InventoryManagement.Web;
using InventoryManagement.Web.U001_ProductManagement.Services;
using InventoryManagement.Web.U002_StockManagement.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// バックエンドAPIのベースURL（appsettings.jsonで環境ごとに切り替え可能）
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
builder.Services.AddScoped<ProductApiClient>();
builder.Services.AddScoped<StockApiClient>();

await builder.Build().RunAsync();
