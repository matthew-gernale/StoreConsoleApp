
namespace Store.Client.ClientServices.ClientInventoryManagers
{
    public class ClientInventoryManager : IClientInventoryManager
    {
        private readonly HttpClient _http;

        public ClientInventoryManager(HttpClient http)
        {
            _http = http;
        }

        public async Task<GeneralResponse<object>> AddProduct(Product product)
        {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/inventorymanager/add-product", product);

            var response_data = await response.Content.ReadFromJsonAsync<GeneralResponse<object>>();
            return response_data ?? new GeneralResponse<object>();
        }

        public async Task<GeneralResponse<object>> RemoveProduct(int productId)
        {
            HttpResponseMessage response = await _http.DeleteAsync($"api/inventorymanager/remove-product/{productId}");

            var response_data = await response.Content.ReadFromJsonAsync<GeneralResponse<object>>();
            return response_data ?? new GeneralResponse<object>();
        }

        public async Task<GeneralResponse<object>> UpdateProduct(int productId, int newQuantity)
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/inventorymanager/update-product/{productId}", newQuantity);

            var response_data = await response.Content.ReadFromJsonAsync<GeneralResponse<object>>();
            return response_data ?? new GeneralResponse<object>();
        }
        public async Task<GeneralResponse<List<ProductDTO>>> ListProducts()
        {
            HttpResponseMessage response = await _http.GetAsync("api/inventorymanager/get-all-products");

            var response_data = await response.Content.ReadFromJsonAsync<GeneralResponse<List<ProductDTO>>>();
            return response_data ?? new GeneralResponse<List<ProductDTO>>();
        }

        public async Task<GeneralResponse<int>> GetTotalValue()
        {
            HttpResponseMessage response = await _http.GetAsync("api/inventorymanager/get-total-value");

            var response_data = await response.Content.ReadFromJsonAsync<GeneralResponse<int>>();
            return response_data ?? new GeneralResponse<int>();
        }
    }
}
