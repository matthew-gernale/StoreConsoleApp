namespace Store.Server.Services.ConversionServices
{
    public interface IConversionService
    {
        ProductDTO ToProductDTO(Product dbProduct);
    }
}
