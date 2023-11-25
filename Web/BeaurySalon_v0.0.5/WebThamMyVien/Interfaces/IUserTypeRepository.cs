using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IUserTypeRepository
    {
        Task<ICollection<UserTypeDto>> GetAllUserType();
        Task<UserTypeDto> GetUserType(int id);
        Task<bool> CreateUserType(UserTypeDto UserType);
        Task<bool> UpdateUserType(UserTypeDto UserType);
        Task<bool> DeleteUserType(UserTypeDto UserType);

    }
}
