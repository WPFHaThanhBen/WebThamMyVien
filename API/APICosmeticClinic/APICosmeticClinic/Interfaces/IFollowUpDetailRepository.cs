using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IFollowUpDetailRepository
    {
        ICollection<FollowUpDetail> GetAllFollowUpDetail();
        FollowUpDetail GetFollowUpDetail(int id);
        bool FollowUpDetailExists(int id);
        bool CreateFollowUpDetail(FollowUpDetail followUpDetail);
        bool UpdateFollowUpDetail(FollowUpDetail followUpDetail);
        bool DeleteFollowUpDetail(FollowUpDetail followUpDetail);
        bool Save();
    }
}
