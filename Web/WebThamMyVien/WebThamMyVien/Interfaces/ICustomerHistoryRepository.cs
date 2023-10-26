using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface ICustomerHistoryRepository
    {
        Task<ICollection<CustomerHistoryDto>> GetAllCustomerHistory();
        Task<CustomerHistoryDto> GetCustomerHistory(int id);
        Task<bool> CreateCustomerHistory(CustomerHistoryDto CustomerHistory);
        Task<bool> UpdateCustomerHistory(CustomerHistoryDto CustomerHistory);
        Task<bool> DeleteCustomerHistory(CustomerHistoryDto CustomerHistory);

    }
}
