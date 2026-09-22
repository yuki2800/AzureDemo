using InventoryManagement.Web;
using InventoryManagement.Web.U001_ProductManagement.Services;
using InventoryManagement.Web.U002_StockManagement.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// バックエンドAPIのベースURL（appsettings.jsonで環境ごとに切り替え可能。相対パスも現在オリジン基準で解決される）
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress;
var resolvedApiBaseUri = new Uri(new Uri(builder.HostEnvironment.BaseAddress), apiBaseUrl);

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = resolvedApiBaseUri });
builder.Services.AddScoped<ProductApiClient>();
builder.Services.AddScoped<StockApiClient>();

await builder.Build().RunAsync();
