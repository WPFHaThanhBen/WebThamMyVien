using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IUserRepository
    {
        ICollection<UserDto> GetAllUser();
        UserDto GetUser(int id);
        bool CreateUser(UserDto user);
        bool UpdateUser(UserDto user);
        bool DeleteUser(UserDto user);
 
    }
}
