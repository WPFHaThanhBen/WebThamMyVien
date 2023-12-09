using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<ICollection<UserAccountDto>> GetAllUserAccount();
        Task<UserAccountDto> GetUserAccount(int id);
        Task<UserAccountDto> GetUserAccountByUserName(string userName);
        Task<bool> CreateUserAccount(UserAccountDto UserAccount);
        Task<bool> Login(string userName, string passWord);
        Task<bool> UpdateUserAccount(UserAccountDto UserAccount);
        Task<bool> DeleteUserAccount(UserAccountDto UserAccount);

    }
}
