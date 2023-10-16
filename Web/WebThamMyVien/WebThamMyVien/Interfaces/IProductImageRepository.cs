using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IProductImageRepository
    {
        ICollection<ProductImageDto> GetAllProductImage();
        ProductImageDto GetProductImage(int id);
        bool CreateProductImage(ProductImageDto productImage);
        bool UpdateProductImage(ProductImageDto productImage);
        bool DeleteProductImage(ProductImageDto productImage);
 
    }
}
