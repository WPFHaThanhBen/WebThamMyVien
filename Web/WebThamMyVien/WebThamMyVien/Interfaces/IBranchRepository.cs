using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IBranchRepository
    {
        ICollection<BranchDto> GetAllBranch();
        BranchDto GetBranch(int id);
        bool CreateBranch(BranchDto branch);
        bool UpdateBranch(BranchDto branch);
        bool DeleteBranch(BranchDto branch);
 
    } 
}
