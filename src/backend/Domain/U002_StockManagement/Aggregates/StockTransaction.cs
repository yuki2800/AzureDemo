using InventoryManagement.Api.Domain.Shared.ValueObjects;
using InventoryManagement.Api.Domain.U002_StockManagement.ValueObjects;

namespace InventoryManagement.Api.Domain.U002_StockManagement.Aggregates;

/// <summary>入出庫履歴。</summary>
public sealed class StockTransaction
{
    public int StockTransactionId { get; private set; }

    public int StockId { get; }

    public int ProductId { get; }

    public TransactionType TransactionType { get; }

    public Weight Quantity { get; }

    public DateTime TransactionDatetime { get; }

    private StockTransaction(
        int stockTransactionId,
        int stockId,
        int productId,
        TransactionType transactionType,
        Weight quantity,
        DateTime transactionDatetime)
    {
        StockTransactionId = stockTransactionId;
        StockId = stockId;
        ProductId = productId;
        TransactionType = transactionType;
        Quantity = quantity;
        TransactionDatetime = transactionDatetime;
    }

    public static StockTransaction Create(
        int stockId,
        int productId,
        TransactionType transactionType,
        Weight quantity,
        DateTime transactionDatetime)
    {
        ArgumentNullException.ThrowIfNull(quantity);

        if (transactionType == TransactionType.None)
        {
            throw new ArgumentException("入出庫区分を指定してください。", nameof(transactionType));
        }

        return new StockTransaction(0, stockId, productId, transactionType, quantity, transactionDatetime);
    }
}
