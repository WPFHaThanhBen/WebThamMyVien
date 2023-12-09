using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public UserAccountRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool UserAccountExists(int id)
        {
            return _context.UserAccounts.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateUserAccount(UserAccount userAccount)
        {
            _context.Add(userAccount);
            return Save();
        }

        public bool DeleteUserAccount(UserAccount userAccount)
        {
            userAccount.IsDelete = true;
            userAccount.DateDelete = sp.GetCurrentDate();
            _context.Update(userAccount);
            return Save();
        }

        public ICollection<UserAccount> GetAllUserAccount()
        {
            return _context.UserAccounts.Where(c => c.IsDelete != true).ToList();
        }

        public UserAccount GetUserAccount(int id)
        {
            return _context.UserAccounts.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUserAccount(UserAccount userAccount)
        {
            _context.Update(userAccount);
            return Save();
        }

        public UserAccount GetUserAccountByUserName(string userName)
        {
            return _context.UserAccounts.Where(e => e.Username == userName && e.IsDelete != true).FirstOrDefault();
        }

        public bool LoginAccount(string userName, string passWord)
        {
            bool result = false;
            var n =  _context.UserAccounts.Where(e => e.Username == userName && e.Password == passWord && e.IsDelete != true).ToList();
            if(n != null)
            {
                if(n.Count() >= 1)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
