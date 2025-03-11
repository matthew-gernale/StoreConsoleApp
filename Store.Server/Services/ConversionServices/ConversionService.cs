namespace Store.Server.Services.ConversionServices
{
    public class ConversionService : IConversionService
    {
        public ProductDTO ToProductDTO(Product dbProduct)
        {
            return new ProductDTO
            {
                Id = dbProduct.Id,
                Name = dbProduct.Name,
                QuantityInStock = dbProduct.QuantityInStock,
                Price = dbProduct.Price,
            };
        }
    }
}
