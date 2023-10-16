using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IUserTypeRepository
    {
        ICollection<UserType> GetAllUserType();
        UserType GetUserType(int id);
        bool UserTypeExists(int id);
        bool CreateUserType(UserType userType);
        bool UpdateUserType(UserType userType);
        bool DeleteUserType(UserType userType);
        bool Save();
    }
}
