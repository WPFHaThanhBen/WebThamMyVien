using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IUserStatusRepository
    {
        ICollection<UserStatus> GetAllUserStatus();
        UserStatus GetUserStatus(int id);
        bool UserStatusExists(int id);
        bool CreateUserStatus(UserStatus userStatus);
        bool UpdateUserStatus(UserStatus userStatus);
        bool DeleteUserStatus(UserStatus userStatus);
        bool Save();
    }
}
