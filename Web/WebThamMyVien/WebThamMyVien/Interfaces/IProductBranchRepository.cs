using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IProductBranchRepository
    {
        Task<ICollection<ProductBranchDto>> GetAllProductBranch();
        Task<ProductBranchDto> GetProductBranch(int id);
        Task<bool> CreateProductBranch(ProductBranchDto ProductBranch);
        Task<bool> UpdateProductBranch(ProductBranchDto ProductBranch);
        Task<bool> DeleteProductBranch(ProductBranchDto ProductBranch);

    }
}
