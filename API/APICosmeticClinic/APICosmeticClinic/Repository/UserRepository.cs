using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class UserRepository : IUserRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public UserRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool UserExists(int id)
        {
            return _context.Users.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            user.IsDelete = true;
            user.DateDelete = sp.GetCurrentDate();
            _context.Update(user);
            return Save();
        }

        public ICollection<User> GetAllUser()
        {
            return _context.Users.Where(c => c.IsDelete != true).ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
