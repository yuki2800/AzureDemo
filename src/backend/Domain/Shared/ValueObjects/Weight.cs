namespace InventoryManagement.Api.Domain.Shared.ValueObjects;

/// <summary>入出庫数量（kg）を表す値オブジェクト。0は無意味な取引のため許容しない。</summary>
public sealed record Weight
{
    public decimal Value { get; }

    private Weight(decimal value)
    {
        Value = value;
    }

    public static Weight Of(decimal value)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "入出庫数量は0より大きい値で指定してください。");
        }

        return new Weight(Math.Round(value, 3, MidpointRounding.AwayFromZero));
    }

    public override string ToString() => Value.ToString("F3");
}
