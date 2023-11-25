using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<ICollection<UserAccountDto>> GetAllUserAccount();
        Task<UserAccountDto> GetUserAccount(int id);
        Task<bool> CreateUserAccount(UserAccountDto UserAccount);
        Task<bool> UpdateUserAccount(UserAccountDto UserAccount);
        Task<bool> DeleteUserAccount(UserAccountDto UserAccount);

    }
}
