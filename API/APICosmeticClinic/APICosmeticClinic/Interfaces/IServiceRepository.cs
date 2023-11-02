using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IServiceRepository
    {
        ICollection<Service> GetAllService();
        ICollection<Service> GetAllServiceByType(int id);
        Service GetService(int id);
        bool ServiceExists(int id);
        bool CreateService(Service service);
        bool UpdateService(Service service);
        bool DeleteService(Service service);
        bool Save();
    }
}
