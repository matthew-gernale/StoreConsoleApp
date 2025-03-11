
namespace Store.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryManagerController : ControllerBase
    {
        private readonly IInventoryManager _inventoryManager;
        private readonly IResponseHelper _responseHelper;

        public InventoryManagerController(IInventoryManager inventoryManager, 
            IResponseHelper responseHelper)
        {
            _inventoryManager = inventoryManager;
            _responseHelper = responseHelper;
        }

        [HttpPost("add-product")]
        public async Task<ActionResult<GeneralResponse<object>>> AddProduct([FromBody] Product product)
        {
            GeneralResponse<object> response = await _inventoryManager.AddProduct(product);
            return _responseHelper.GetStatusResponse(response);
        }

        [HttpDelete("remove-product/{productId}")]
        public async Task<ActionResult<GeneralResponse<object>>> RemoveProduct(int productId)
        {
            GeneralResponse<object> response = await _inventoryManager.RemoveProduct(productId);
            return _responseHelper.GetStatusResponse(response);
        }

        [HttpPut("update-product/{productId}")]
        public async Task<ActionResult<GeneralResponse<object>>> UpdateProduct(int productId, [FromBody] int newQuantity)
        {
            GeneralResponse<object> response = await _inventoryManager.UpdateProduct(productId, newQuantity);
            return _responseHelper.GetStatusResponse(response);
        }

        [HttpGet("get-all-products")]
        public async Task<ActionResult<GeneralResponse<List<ProductDTO>>>> ListProducts()
        {
            GeneralResponse<List<ProductDTO>> response = await _inventoryManager.ListProducts();
            return _responseHelper.GetStatusResponseWData(response);
        }

        [HttpGet("get-total-value")]
        public async Task<ActionResult<GeneralResponse<int>>> GetTotalValue()
        {
            GeneralResponse<int> response = await _inventoryManager.GetTotalValue();
            return _responseHelper.GetStatusResponseWData(response);
        }
    }
}
