namespace InventoryManagement.Api.Domain.U001_ProductManagement.Exceptions;

/// <summary>指定された商品が商品マスタに存在しない場合にスローされる。</summary>
public sealed class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int productId)
        : base($"商品が見つかりません。ProductId: {productId}")
    {
    }
}
