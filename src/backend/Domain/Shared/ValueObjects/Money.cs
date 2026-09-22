namespace InventoryManagement.Api.Domain.Shared.ValueObjects;

/// <summary>金額（円）を表す値オブジェクト。</summary>
public sealed record Money
{
    public decimal Amount { get; }

    private Money(decimal amount)
    {
        Amount = amount;
    }

    public static Money Zero { get; } = new(0m);

    public static Money Of(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "単価は0以上で指定してください。");
        }

        return new Money(Math.Round(amount, 2, MidpointRounding.AwayFromZero));
    }

    public override string ToString() => Amount.ToString("F2");
}
