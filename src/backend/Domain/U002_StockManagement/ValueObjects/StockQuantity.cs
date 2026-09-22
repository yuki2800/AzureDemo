namespace InventoryManagement.Api.Domain.U002_StockManagement.ValueObjects;

/// <summary>在庫の現在庫数量（kg）を表す値オブジェクト。在庫切れ状態を表現するため0を許容する。</summary>
public sealed record StockQuantity
{
    public decimal Value { get; }

    private StockQuantity(decimal value)
    {
        Value = value;
    }

    public static StockQuantity Zero { get; } = new(0m);

    public static StockQuantity Of(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "在庫数量は0以上で指定してください。");
        }

        return new StockQuantity(Math.Round(value, 3, MidpointRounding.AwayFromZero));
    }

    public override string ToString() => Value.ToString("F3");
}
