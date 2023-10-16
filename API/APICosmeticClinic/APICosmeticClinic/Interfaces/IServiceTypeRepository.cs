using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IServiceTypeRepository
    {
        ICollection<ServiceType> GetAllServiceType();
        ServiceType GetServiceType(int id);
        bool ServiceTypeExists(int id);
        bool CreateServiceType(ServiceType serviceType);
        bool UpdateServiceType(ServiceType serviceType);
        bool DeleteServiceType(ServiceType serviceType);
        bool Save();
    }
}
