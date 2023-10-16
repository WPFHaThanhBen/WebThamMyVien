using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IUserStatusRepository
    {
        ICollection<UserStatusDto> GetAllUserStatus();
        UserStatusDto GetUserStatus(int id);
        bool CreateUserStatus(UserStatusDto userStatus);
        bool UpdateUserStatus(UserStatusDto userStatus);
        bool DeleteUserStatus(UserStatusDto userStatus);
 
    }
}
