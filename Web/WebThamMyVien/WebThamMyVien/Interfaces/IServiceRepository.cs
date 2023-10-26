using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IServiceRepository
    {
        Task<ICollection<ServiceDto>> GetAllService();
        Task<ServiceDto> GetService(int id);
        Task<bool> CreateService(ServiceDto service);
        Task<bool> UpdateService(ServiceDto service);
        Task<bool> DeleteService(ServiceDto service);

    }
}
