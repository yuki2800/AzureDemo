using System.Net.Http.Json;
using System.Text.Json;

namespace InventoryManagement.Web.Common;

/// <summary>APIエラーレスポンス（ProblemDetails形式）からエラーメッセージを抽出する。</summary>
public static class ApiErrorReader
{
    public static async Task<string> ReadErrorMessageAsync(HttpResponseMessage response)
    {
        try
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            if (errorResponse is not null)
            {
                if (!string.IsNullOrEmpty(errorResponse.Detail))
                {
                    return errorResponse.Detail;
                }

                if (errorResponse.Errors is { Count: > 0 })
                {
                    return string.Join(" ", errorResponse.Errors.SelectMany(x => x.Value));
                }

                if (!string.IsNullOrEmpty(errorResponse.Title))
                {
                    return errorResponse.Title;
                }
            }
        }
        catch (JsonException)
        {
            // 本文がJSON形式でない場合は無視し、汎用メッセージを返す。
        }

        return "処理に失敗しました。時間をおいて再度お試しください。";
    }

    private sealed class ApiErrorResponse
    {
        public string? Title { get; set; }

        public string? Detail { get; set; }

        public Dictionary<string, string[]>? Errors { get; set; }
    }
}
