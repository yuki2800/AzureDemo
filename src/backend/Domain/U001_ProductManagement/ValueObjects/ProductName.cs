namespace InventoryManagement.Api.Domain.U001_ProductManagement.ValueObjects;

/// <summary>商品名を表す値オブジェクト。</summary>
public sealed record ProductName
{
    public string Value { get; }

    private ProductName(string value)
    {
        Value = value;
    }

    public static ProductName Of(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length > 100)
        {
            throw new ArgumentException("商品名は1〜100文字で指定してください。", nameof(value));
        }

        return new ProductName(value);
    }

    public override string ToString() => Value;
}
