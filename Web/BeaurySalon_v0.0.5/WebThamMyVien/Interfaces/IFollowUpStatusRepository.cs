using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IFollowUpStatusRepository
    {
        Task<ICollection<FollowUpStatusDto>> GetAllFollowUpStatus();
        Task<FollowUpStatusDto> GetFollowUpStatus(int id);
        Task<bool> CreateFollowUpStatus(FollowUpStatusDto FollowUpStatus);
        Task<bool> UpdateFollowUpStatus(FollowUpStatusDto FollowUpStatus);
        Task<bool> DeleteFollowUpStatus(FollowUpStatusDto FollowUpStatus);

    }
}
