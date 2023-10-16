using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IProductTypeRepository
    {
        ICollection<ProductTypeDto> GetAllProductType();
        ProductTypeDto GetProductType(int id);
        bool CreateProductType(ProductTypeDto productType);
        bool UpdateProductType(ProductTypeDto productType);
        bool DeleteProductType(ProductTypeDto productType);
 
    }
}
