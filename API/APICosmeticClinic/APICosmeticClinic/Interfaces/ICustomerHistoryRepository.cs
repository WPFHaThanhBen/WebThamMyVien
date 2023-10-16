using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface ICustomerHistoryRepository
    {
        ICollection<CustomerHistory> GetAllCustomerHistory();
        CustomerHistory GetCustomerHistory(int id);
        bool CustomerHistoryExists(int id);
        bool CreateCustomerHistory(CustomerHistory customerHistory);
        bool UpdateCustomerHistory(CustomerHistory customerHistory);
        bool DeleteCustomerHistory(CustomerHistory customerHistory);
        bool Save();
    }
}
