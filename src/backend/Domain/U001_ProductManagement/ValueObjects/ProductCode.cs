using System.Text.RegularExpressions;

namespace InventoryManagement.Api.Domain.U001_ProductManagement.ValueObjects;

/// <summary>商品コードを表す値オブジェクト。</summary>
public sealed partial record ProductCode
{
    public string Value { get; }

    private ProductCode(string value)
    {
        Value = value;
    }

    public static ProductCode Of(string value)
    {
        if (string.IsNullOrEmpty(value) || !AlphanumericPattern().IsMatch(value))
        {
            throw new ArgumentException("商品コードは1〜20文字の英数字で指定してください。", nameof(value));
        }

        return new ProductCode(value);
    }

    public override string ToString() => Value;

    [GeneratedRegex(@"^[A-Za-z0-9]{1,20}$")]
    private static partial Regex AlphanumericPattern();
}
