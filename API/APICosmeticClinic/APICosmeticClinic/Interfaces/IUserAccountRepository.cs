using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IUserAccountRepository
    {
        ICollection<UserAccount> GetAllUserAccount();
        UserAccount GetUserAccount(int id);
        bool UserAccountExists(int id);
        bool CreateUserAccount(UserAccount userAccount);
        bool UpdateUserAccount(UserAccount userAccount);
        bool DeleteUserAccount(UserAccount userAccount);
        bool Save();
    }
}
