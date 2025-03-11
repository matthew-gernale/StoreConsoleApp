
namespace Store.Server.Repository.InventoryManager
{
    public interface IInventoryManager
    {
        Task<GeneralResponse<object>> AddProduct(Product product);
        Task<GeneralResponse<object>> RemoveProduct(int productId);
        Task<GeneralResponse<object>> UpdateProduct(int productId, int newQuantity);
        Task<GeneralResponse<List<ProductDTO>>> ListProducts();
        Task<GeneralResponse<int>> GetTotalValue();
    }
}
