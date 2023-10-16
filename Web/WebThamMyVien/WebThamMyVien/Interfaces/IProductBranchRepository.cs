using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IProductBranchRepository
    {
        ICollection<ProductBranchDto> GetAllProductBranch();
        ProductBranchDto GetProductBranch(int id);
        bool CreateProductBranch(ProductBranchDto productBranch);
        bool UpdateProductBranch(ProductBranchDto productBranch);
        bool DeleteProductBranch(ProductBranchDto productBranch);
 
    }
}
