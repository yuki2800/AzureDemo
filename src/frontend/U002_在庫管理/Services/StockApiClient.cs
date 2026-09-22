using System.Net.Http.Json;
using InventoryManagement.Web.Common;
using InventoryManagement.Web.U002_StockManagement.Models;

namespace InventoryManagement.Web.U002_StockManagement.Services;

public sealed class StockApiClient
{
    private readonly HttpClient _httpClient;

    public StockApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResult<List<StockDto>>> GetStocksAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/stocks");
            if (!response.IsSuccessStatusCode)
            {
                return ApiResult<List<StockDto>>.Failure(await ApiErrorReader.ReadErrorMessageAsync(response));
            }

            var stocks = await response.Content.ReadFromJsonAsync<List<StockDto>>();
            return ApiResult<List<StockDto>>.Success(stocks ?? new List<StockDto>());
        }
        catch (HttpRequestException)
        {
            return ApiResult<List<StockDto>>.Failure("APIサーバーに接続できませんでした。");
        }
    }

    public async Task<ApiResult> RegisterStockTransactionAsync(StockTransactionRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/stocks/transactions", request);
            if (!response.IsSuccessStatusCode)
            {
                return ApiResult.Failure(await ApiErrorReader.ReadErrorMessageAsync(response));
            }

            return ApiResult.Success();
        }
        catch (HttpRequestException)
        {
            return ApiResult.Failure("APIサーバーに接続できませんでした。");
        }
    }
}
