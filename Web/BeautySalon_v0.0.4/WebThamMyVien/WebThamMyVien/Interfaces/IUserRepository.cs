using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<UserDto>> GetAllUser();
        Task<UserDto> GetUser(int id);
        Task<bool> CreateUser(UserDto User);
        Task<bool> UpdateUser(UserDto User);
        Task<bool> DeleteUser(UserDto User);
 
    }
}
