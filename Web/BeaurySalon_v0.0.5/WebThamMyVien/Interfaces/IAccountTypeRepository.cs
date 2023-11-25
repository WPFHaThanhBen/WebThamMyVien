using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IAccountTypeRepository
    {
        Task<ICollection<AccountTypeDto>> GetAllAccountType();
        Task<AccountTypeDto> GetAccountType(int id);
        Task<bool> CreateAccountType(AccountTypeDto accountType);
        Task<bool> UpdateAccountType(AccountTypeDto accountType);
        Task<bool> DeleteAccountType(AccountTypeDto accountType);
 
    }
}
