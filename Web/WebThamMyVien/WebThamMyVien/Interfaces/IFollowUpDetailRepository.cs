using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IFollowUpDetailRepository
    {
        ICollection<FollowUpDetailDto> GetAllFollowUpDetail();
        FollowUpDetailDto GetFollowUpDetail(int id);
        bool CreateFollowUpDetail(FollowUpDetailDto followUpDetail);
        bool UpdateFollowUpDetail(FollowUpDetailDto followUpDetail);
        bool DeleteFollowUpDetail(FollowUpDetailDto followUpDetail);
 
    }
}
