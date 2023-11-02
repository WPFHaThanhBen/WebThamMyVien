using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetAllProduct();
        ICollection<Product> GetAllProductByType(int id);
        Product GetProduct(int id);
        bool ProductExists(int id);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool Save();
    }
}
