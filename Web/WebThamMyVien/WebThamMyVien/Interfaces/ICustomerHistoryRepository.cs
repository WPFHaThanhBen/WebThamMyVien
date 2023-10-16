using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface ICustomerHistoryRepository
    {
        ICollection<CustomerHistoryDto> GetAllCustomerHistory();
        CustomerHistoryDto GetCustomerHistory(int id);
        bool CreateCustomerHistory(CustomerHistoryDto customerHistory);
        bool UpdateCustomerHistory(CustomerHistoryDto customerHistory);
        bool DeleteCustomerHistory(CustomerHistoryDto customerHistory);
 
    }
}
