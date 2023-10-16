using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public UserTypeRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool UserTypeExists(int id)
        {
            return _context.UserTypes.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateUserType(UserType userType)
        {
            _context.Add(userType);
            return Save();
        }

        public bool DeleteUserType(UserType userType)
        {
            userType.IsDelete = true;
            userType.DateDelete = sp.GetCurrentDate();
            _context.Update(userType);
            return Save();
        }

        public ICollection<UserType> GetAllUserType()
        {
            return _context.UserTypes.Where(c => c.IsDelete != true).ToList();
        }

        public UserType GetUserType(int id)
        {
            return _context.UserTypes.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUserType(UserType userType)
        {
            _context.Update(userType);
            return Save();
        }
    }
}
