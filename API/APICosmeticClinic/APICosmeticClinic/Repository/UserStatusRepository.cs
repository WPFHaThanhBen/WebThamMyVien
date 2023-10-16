using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class UserStatusRepository : IUserStatusRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public UserStatusRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool UserStatusExists(int id)
        {
            return _context.UserStatuses.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateUserStatus(UserStatus userStatus)
        {
            _context.Add(userStatus);
            return Save();
        }

        public bool DeleteUserStatus(UserStatus userStatus)
        {
            userStatus.IsDelete = true;
            userStatus.DateDelete = sp.GetCurrentDate();
            _context.Update(userStatus);
            return Save();
        }

        public ICollection<UserStatus> GetAllUserStatus()
        {
            return _context.UserStatuses.Where(c => c.IsDelete != true).ToList();
        }

        public UserStatus GetUserStatus(int id)
        {
            return _context.UserStatuses.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUserStatus(UserStatus userStatus)
        {
            _context.Update(userStatus);
            return Save();
        }
    }
}
