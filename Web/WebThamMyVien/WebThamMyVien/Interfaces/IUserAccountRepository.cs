using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IUserAccountRepository
    {
        ICollection<UserAccountDto> GetAllUserAccount();
        UserAccountDto GetUserAccount(int id);
        bool CreateUserAccount(UserAccountDto userAccount);
        bool UpdateUserAccount(UserAccountDto userAccount);
        bool DeleteUserAccount(UserAccountDto userAccount);
 
    }
}
