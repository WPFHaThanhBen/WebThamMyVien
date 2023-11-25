using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<ProductDto>> GetAllProduct();
        Task<ProductDto> GetProduct(int id);
        Task<bool> CreateProduct(ProductDto Product);
        Task<bool> UpdateProduct(ProductDto Product);
        Task<bool> DeleteProduct(ProductDto Product);

    }
}
