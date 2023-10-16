using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface ICustomerStatusRepository
    {
        ICollection<CustomerStatus> GetAllCustomerStatus();
        CustomerStatus GetCustomerStatus(int id);
        bool CustomerStatusExists(int id);
        bool CreateCustomerStatus(CustomerStatus customerStatus);
        bool UpdateCustomerStatus(CustomerStatus customerStatus);
        bool DeleteCustomerStatus(CustomerStatus customerStatus);
        bool Save();
    }
}
