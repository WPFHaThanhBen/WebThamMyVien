using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IAccountTypeRepository
    {
        ICollection<AccountType> GetAllAccountType();
        AccountType GetAccountType(int id);
        bool AccountTypeExists(int id);
        bool CreateAccountType(AccountType accountType);
        bool UpdateAccountType(AccountType accountType);
        bool DeleteAccountType(AccountType accountType);
        bool Save();
    }
}
