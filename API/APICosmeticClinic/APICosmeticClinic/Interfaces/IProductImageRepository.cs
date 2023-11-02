using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IProductImageRepository
    {
        ICollection<ProductImage> GetAllProductImage();
        ICollection<ProductImage> GetAllProductImageByProduct(int id);
        ProductImage GetProductImage(int id);
        bool ProductImageExists(int id);
        bool CreateProductImage(ProductImage productImage);
        bool UpdateProductImage(ProductImage productImage);
        bool DeleteProductImage(ProductImage productImage);
        bool Save();
    }
}
