using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IServiceTypeRepository
    {
        ICollection<ServiceTypeDto> GetAllServiceType();
        ServiceTypeDto GetServiceType(int id);
        bool CreateServiceType(ServiceTypeDto serviceType);
        bool UpdateServiceType(ServiceTypeDto serviceType);
        bool DeleteServiceType(ServiceTypeDto serviceType);
 
    }
}
