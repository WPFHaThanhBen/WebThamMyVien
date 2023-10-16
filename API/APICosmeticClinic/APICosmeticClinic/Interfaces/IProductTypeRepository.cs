using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IProductTypeRepository
    {
        ICollection<ProductType> GetAllProductType();
        ProductType GetProductType(int id);
        bool ProductTypeExists(int id);
        bool CreateProductType(ProductType productType);
        bool UpdateProductType(ProductType productType);
        bool DeleteProductType(ProductType productType);
        bool Save();
    }
}
