using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface ICustomerRepository
    {
        ICollection<CustomerDto> GetAllCustomer();
        CustomerDto GetCustomer(int id);
        bool CreateCustomer(CustomerDto customer);
        bool UpdateCustomer(CustomerDto customer);
        bool DeleteCustomer(CustomerDto customer);
 
    }
}
