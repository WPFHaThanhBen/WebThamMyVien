using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IProductImageRepository
    {
        ICollection<ProductImage> GetAllProductImage();
        ProductImage GetProductImage(int id);
        bool ProductImageExists(int id);
        bool CreateProductImage(ProductImage productImage);
        bool UpdateProductImage(ProductImage productImage);
        bool DeleteProductImage(ProductImage productImage);
        bool Save();
    }
}
