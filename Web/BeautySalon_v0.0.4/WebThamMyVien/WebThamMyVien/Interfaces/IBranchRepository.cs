using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IBranchRepository
    {
        Task<ICollection<BranchDto>> GetAllBranch();
        Task<BranchDto> GetBranch(int id);
        Task<bool> CreateBranch(BranchDto Branch);
        Task<bool> UpdateBranch(BranchDto Branch);
        Task<bool> DeleteBranch(BranchDto Branch);

    } 
}
