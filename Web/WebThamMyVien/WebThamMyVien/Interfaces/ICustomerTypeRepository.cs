using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface ICustomerTypeRepository
    {
        ICollection<CustomerTypeDto> GetAllCustomerType();
        CustomerTypeDto GetCustomerType(int id);
        bool CreateCustomerType(CustomerTypeDto customerType);
        bool UpdateCustomerType(CustomerTypeDto customerType);
        bool DeleteCustomerType(CustomerTypeDto customerType);
 
    }
}
