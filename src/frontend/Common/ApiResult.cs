namespace InventoryManagement.Web.Common;

/// <summary>APIレスポンス（データなし）の成否を表す。</summary>
public sealed class ApiResult
{
    public bool IsSuccess { get; init; }

    public string? ErrorMessage { get; init; }

    public static ApiResult Success() => new() { IsSuccess = true };

    public static ApiResult Failure(string errorMessage) => new() { IsSuccess = false, ErrorMessage = errorMessage };
}

/// <summary>APIレスポンス（データあり）の成否と取得データを表す。</summary>
public sealed class ApiResult<T>
{
    public bool IsSuccess { get; init; }

    public T? Value { get; init; }

    public string? ErrorMessage { get; init; }

    public static ApiResult<T> Success(T value) => new() { IsSuccess = true, Value = value };

    public static ApiResult<T> Failure(string errorMessage) => new() { IsSuccess = false, ErrorMessage = errorMessage };
}
