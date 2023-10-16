using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IProductBranchRepository
    {
        ICollection<ProductBranch> GetAllProductBranch();
        ProductBranch GetProductBranch(int id);
        bool ProductBranchExists(int id);
        bool CreateProductBranch(ProductBranch productBranch);
        bool UpdateProductBranch(ProductBranch productBranch);
        bool DeleteProductBranch(ProductBranch productBranch);
        bool Save();
    }
}
