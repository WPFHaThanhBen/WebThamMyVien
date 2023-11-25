using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IUserStatusRepository
    {
        Task<ICollection<UserStatusDto>> GetAllUserStatus();
        Task<UserStatusDto> GetUserStatus(int id);
        Task<bool> CreateUserStatus(UserStatusDto UserStatus);
        Task<bool> UpdateUserStatus(UserStatusDto UserStatus);
        Task<bool> DeleteUserStatus(UserStatusDto UserStatus);

    }
}
