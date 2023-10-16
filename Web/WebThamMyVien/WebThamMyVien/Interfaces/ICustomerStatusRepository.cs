using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface ICustomerStatusRepository
    {
        ICollection<CustomerStatusDto> GetAllCustomerStatus();
        CustomerStatusDto GetCustomerStatus(int id);
        bool CreateCustomerStatus(CustomerStatusDto customerStatus);
        bool UpdateCustomerStatus(CustomerStatusDto customerStatus);
        bool DeleteCustomerStatus(CustomerStatusDto customerStatus);
 
    }
}
