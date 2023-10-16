using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IFollowUpStatusRepository
    {
        ICollection<FollowUpStatusDto> GetAllFollowUpStatus();
        FollowUpStatusDto GetFollowUpStatus(int id);
        bool CreateFollowUpStatus(FollowUpStatusDto followUpStatus);
        bool UpdateFollowUpStatus(FollowUpStatusDto followUpStatus);
        bool DeleteFollowUpStatus(FollowUpStatusDto followUpStatus);
 
    }
}
