
namespace Store.Server.Repository.InventoryManager
{
    public class InventoryManager : IInventoryManager
    {
        private readonly DataContext _context;
        private readonly IResponseHelper _responseHelper;
        private readonly IConversionService _convert;

        public InventoryManager(DataContext context,
            IResponseHelper responseHelper,
            IConversionService convert)
        {
            _context = context;
            _responseHelper = responseHelper;
            _convert = convert;
        }

        public async Task<GeneralResponse<object>> AddProduct(Product product)
        {
            try
            {
                if (await _context.Products.AnyAsync(p => p.Name == product.Name))
                    return _responseHelper.ErrorResponse($"{product.Name} is already existing in the product list.", HttpStatusCode.Conflict);

                _context.Products.Add(product);
                int result = await _context.SaveChangesAsync();

                return result > 0
                    ? _responseHelper.SuccessResponse()
                    : _responseHelper.ErrorResponse($"Failed to save {product.Name} product to the database.", HttpStatusCode.Conflict);
            }
            catch
            {
                return _responseHelper.ErrorResponse($"An error occured while creating {product.Name} product.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<GeneralResponse<object>> RemoveProduct(int productId)
        {
            try
            {
                Product? dbProduct = await _context.Products
                    .FirstOrDefaultAsync(product => product.Id == productId);

                if (dbProduct == null)
                    return _responseHelper.ErrorResponse($"Product with #{productId} ID doesn't exist.", HttpStatusCode.NotFound);

                _context.Products.Remove(dbProduct);
                int result = await _context.SaveChangesAsync();

                return result > 0
                    ? _responseHelper.SuccessResponse()
                    : _responseHelper.ErrorResponse($"Deleted {dbProduct.Name} not saved to the database.", HttpStatusCode.Conflict);
            }
            catch
            {
                return _responseHelper.ErrorResponse($"An error occured while removing product #{productId}.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<GeneralResponse<object>> UpdateProduct(int productId, int newQuantity)
        {
            try
            {
                Product? dbProduct = await _context.Products
                    .FirstOrDefaultAsync(product => product.Id == productId);

                if (dbProduct == null)
                    return _responseHelper.ErrorResponse($"Product with #{productId} ID doesn't exist.", HttpStatusCode.NotFound);

                dbProduct.QuantityInStock = newQuantity;
                int result = await _context.SaveChangesAsync();

                return result > 0
                    ? _responseHelper.SuccessResponse()
                    : _responseHelper.ErrorResponse($"Updated {dbProduct.Name} quantity not saved to the database.", HttpStatusCode.Conflict);
            }
            catch
            {
                return _responseHelper.ErrorResponse($"An error occured while updating product #{productId}.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<GeneralResponse<List<ProductDTO>>> ListProducts()
        {
            try
            {
                List<Product>? dbProducts = await _context.Products.ToListAsync();
                if (dbProducts == null)
                    return _responseHelper.NoContentResponseWData<List<ProductDTO>>();

                List<ProductDTO> response = dbProducts.Select(product => _convert.ToProductDTO(product))
                    .ToList();
                return _responseHelper.SuccessResponseWData(response);
            }
            catch
            {
                return _responseHelper.ErrorResponseWData<List<ProductDTO>>("Failed to fetch products.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<GeneralResponse<int>> GetTotalValue()
        {
            try
            {
                int totalValue = await _context.Products
                    .Select(product => product.QuantityInStock)
                    .SumAsync();

                return _responseHelper.SuccessResponseWData(totalValue);
            }
            catch
            {
                return _responseHelper.ErrorResponseWData<int>("Failed to fetch total value.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
