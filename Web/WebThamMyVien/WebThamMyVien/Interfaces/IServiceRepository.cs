using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IServiceRepository
    {
        ICollection<ServiceDto> GetAllService();
        ServiceDto GetService(int id);
        bool CreateService(ServiceDto service);
        bool UpdateService(ServiceDto service);
        bool DeleteService(ServiceDto service);
 
    }
}
