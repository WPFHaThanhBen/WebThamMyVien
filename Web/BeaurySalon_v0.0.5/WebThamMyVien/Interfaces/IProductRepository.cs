﻿ using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<ProductDto>> GetAllProduct();
        Task<ICollection<ProductDto>> GetAllProductByType(int id);
        Task<ProductDto> GetProduct(int id);
        Task<ProductDto> GetProductFinal();
        Task<bool> CreateProduct(ProductDto Product);
        Task<bool> UpdateProduct(ProductDto Product);
        Task<bool> DeleteProduct(ProductDto Product);

    }
}
