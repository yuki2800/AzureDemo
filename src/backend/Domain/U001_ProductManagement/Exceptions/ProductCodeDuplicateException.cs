namespace InventoryManagement.Api.Domain.U001_ProductManagement.Exceptions;

/// <summary>指定された商品コードが既に登録されている場合にスローされる。</summary>
public sealed class ProductCodeDuplicateException : Exception
{
    public ProductCodeDuplicateException(string productCode)
        : base($"商品コードが重複しています。ProductCode: {productCode}")
    {
    }
}
