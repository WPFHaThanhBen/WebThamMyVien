using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IProductRepository
    {
        ICollection<ProductDto> GetAllProduct();
        ProductDto GetProduct(int id);
        bool CreateProduct(ProductDto product);
        bool UpdateProduct(ProductDto product);
        bool DeleteProduct(ProductDto product);
 
    }
}
