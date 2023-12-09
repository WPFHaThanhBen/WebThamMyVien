using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IUserAccountRepository
    {
        ICollection<UserAccount> GetAllUserAccount();
        UserAccount GetUserAccount(int id);
        UserAccount GetUserAccountByUserName(string userName);
        bool UserAccountExists(int id);
        bool CreateUserAccount(UserAccount userAccount);
        bool LoginAccount(string userName, string passWord);
        bool UpdateUserAccount(UserAccount userAccount);
        bool DeleteUserAccount(UserAccount userAccount);
        bool Save();
    }
}
