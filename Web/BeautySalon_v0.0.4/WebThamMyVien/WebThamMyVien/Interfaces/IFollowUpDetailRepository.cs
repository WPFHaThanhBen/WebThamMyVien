using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IFollowUpDetailRepository
    {
        Task<ICollection<FollowUpDetailDto>> GetAllFollowUpDetail();
        Task<FollowUpDetailDto> GetFollowUpDetail(int id);
        Task<bool> CreateFollowUpDetail(FollowUpDetailDto FollowUpDetail);
        Task<bool> UpdateFollowUpDetail(FollowUpDetailDto FollowUpDetail);
        Task<bool> DeleteFollowUpDetail(FollowUpDetailDto FollowUpDetail);

    }
}
