using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IBranchRepository
    {
        ICollection<Branch> GetAllBranch();
        Branch GetBranch(int id);
        bool BranchExists(int id);
        bool CreateBranch(Branch branch);
        bool UpdateBranch(Branch branch);
        bool DeleteBranch(Branch branch);
        bool Save();
    } 
}
