using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IServiceTypeRepository
    {
        Task<ICollection<ServiceTypeDto>> GetAllServiceType();
        Task<ServiceTypeDto> GetServiceType(int id);
        Task<bool> CreateServiceType(ServiceTypeDto ServiceType);
        Task<bool> UpdateServiceType(ServiceTypeDto ServiceType);
        Task<bool> DeleteServiceType(ServiceTypeDto ServiceType);
 
    }
}
