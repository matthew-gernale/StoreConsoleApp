
namespace Store.Client.ClientServices.ClientInventoryManagers
{
    public interface IClientInventoryManager
    {
        Task<GeneralResponse<object>> AddProduct(Product product);
        Task<GeneralResponse<object>> RemoveProduct(int productId);
        Task<GeneralResponse<object>> UpdateProduct(int productId, int newQuantity);
        Task<GeneralResponse<List<ProductDTO>>> ListProducts();
        Task<GeneralResponse<int>> GetTotalValue();
    }
}
