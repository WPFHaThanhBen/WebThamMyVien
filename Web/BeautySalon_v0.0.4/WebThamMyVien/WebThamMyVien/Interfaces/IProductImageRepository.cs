using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IProductImageRepository
    {
        Task<ICollection<ProductImageDto>> GetAllProductImage();
        Task<ProductImageDto> GetProductImage(int id);
        Task<bool> CreateProductImage(ProductImageDto ProductImage);
        Task<bool> UpdateProductImage(ProductImageDto ProductImage);
        Task<bool> DeleteProductImage(ProductImageDto ProductImage);

    }
}
