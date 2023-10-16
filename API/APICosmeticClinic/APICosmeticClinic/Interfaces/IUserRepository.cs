using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetAllUser();
        User GetUser(int id);
        bool UserExists(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
