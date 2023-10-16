using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IFollowUpStatusRepository
    {
        ICollection<FollowUpStatus> GetAllFollowUpStatus();
        FollowUpStatus GetFollowUpStatus(int id);
        bool FollowUpStatusExists(int id);
        bool CreateFollowUpStatus(FollowUpStatus followUpStatus);
        bool UpdateFollowUpStatus(FollowUpStatus followUpStatus);
        bool DeleteFollowUpStatus(FollowUpStatus followUpStatus);
        bool Save();
    }
}
