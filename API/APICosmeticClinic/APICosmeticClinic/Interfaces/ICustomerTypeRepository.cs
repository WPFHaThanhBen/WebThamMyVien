using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface ICustomerTypeRepository
    {
        ICollection<CustomerType> GetAllCustomerType();
        CustomerType GetCustomerType(int id);
        bool CustomerTypeExists(int id);
        bool CreateCustomerType(CustomerType customerType);
        bool UpdateCustomerType(CustomerType customerType);
        bool DeleteCustomerType(CustomerType customerType);
        bool Save();
    }
}
