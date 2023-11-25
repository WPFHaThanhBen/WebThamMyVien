using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IProductTypeRepository
    {
        Task<ICollection<ProductTypeDto>> GetAllProductType();
        Task<ProductTypeDto> GetProductType(int id);
        Task<bool> CreateProductType(ProductTypeDto ProductType);
        Task<bool> UpdateProductType(ProductTypeDto ProductType);
        Task<bool> DeleteProductType(ProductTypeDto ProductType);

    }
}
