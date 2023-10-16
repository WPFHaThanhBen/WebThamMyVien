using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IUserTypeRepository
    {
        ICollection<UserTypeDto> GetAllUserType();
        UserTypeDto GetUserType(int id);
        bool CreateUserType(UserTypeDto userType);
        bool UpdateUserType(UserTypeDto userType);
        bool DeleteUserType(UserTypeDto userType);
 
    }
}
