using System.Net.Http.Json;
using InventoryManagement.Web.Common;
using InventoryManagement.Web.U001_ProductManagement.Models;

namespace InventoryManagement.Web.U001_ProductManagement.Services;

public sealed class ProductApiClient
{
    private readonly HttpClient _httpClient;

    public ProductApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResult<List<ProductDto>>> GetProductsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/products");
            if (!response.IsSuccessStatusCode)
            {
                return ApiResult<List<ProductDto>>.Failure(await ApiErrorReader.ReadErrorMessageAsync(response));
            }

            var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
            return ApiResult<List<ProductDto>>.Success(products ?? new List<ProductDto>());
        }
        catch (HttpRequestException)
        {
            return ApiResult<List<ProductDto>>.Failure("APIサーバーに接続できませんでした。");
        }
    }

    public async Task<ApiResult<ProductDto>> RegisterProductAsync(RegisterProductRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", request);
            if (!response.IsSuccessStatusCode)
            {
                return ApiResult<ProductDto>.Failure(await ApiErrorReader.ReadErrorMessageAsync(response));
            }

            var product = await response.Content.ReadFromJsonAsync<ProductDto>();
            return ApiResult<ProductDto>.Success(product!);
        }
        catch (HttpRequestException)
        {
            return ApiResult<ProductDto>.Failure("APIサーバーに接続できませんでした。");
        }
    }
}
