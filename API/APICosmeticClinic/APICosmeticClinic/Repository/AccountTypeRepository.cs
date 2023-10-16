using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using APICosmeticClinic.Helper;

namespace APICosmeticClinic.Repository
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public AccountTypeRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }

        public bool AccountTypeExists(int id)
        {
            return _context.AccountTypes.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateAccountType(AccountType accountType)
        {
            _context.Add(accountType);
            return Save();
        }

        public bool DeleteAccountType(AccountType accountType)
        {
            accountType.IsDelete = true;
            accountType.DateDelete = sp.GetCurrentDate();
            _context.Update(accountType);
            return Save();
        }


        public AccountType GetAccountType(int id)
        {
            return _context.AccountTypes.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public ICollection<AccountType> GetAllAccountType()
        {
            return _context.AccountTypes.Where(c => c.IsDelete != true).ToList();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAccountType(AccountType accountType)
        {
            _context.Update(accountType);
            return Save();
        }
    }
}
