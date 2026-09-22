using InventoryManagement.Api.Application.U001_ProductManagement.Services;
using InventoryManagement.Api.Application.U002_StockManagement.Services;
using InventoryManagement.Api.Domain.U001_ProductManagement.Repositories;
using InventoryManagement.Api.Domain.U002_StockManagement.DomainServices;
using InventoryManagement.Api.Domain.U002_StockManagement.Repositories;
using InventoryManagement.Api.Infrastructure.Persistence;
using InventoryManagement.Api.Infrastructure.Repositories;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

const string FrontendCorsPolicy = "FrontendCorsPolicy";

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console(new CompactJsonFormatter());

    var appInsightsConnectionString = context.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
    if (!string.IsNullOrWhiteSpace(appInsightsConnectionString))
    {
        var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
        telemetryConfiguration.ConnectionString = appInsightsConnectionString;

        loggerConfiguration.WriteTo.ApplicationInsights(
            telemetryConfiguration,
            TelemetryConverter.Traces);
    }
});

builder.Services.AddControllers();

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryDb")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();
builder.Services.AddScoped<StockInboundService>();
builder.Services.AddScoped<ProductApplicationService>();
builder.Services.AddScoped<StockApplicationService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:5100")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseCors(FrontendCorsPolicy);
app.MapControllers();

try
{
    Log.Information("アプリケーションを起動します。");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "アプリケーションが予期せず終了しました。");
}
finally
{
    Log.CloseAndFlush();
}
