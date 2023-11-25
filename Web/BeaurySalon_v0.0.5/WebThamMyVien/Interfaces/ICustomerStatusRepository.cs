using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface ICustomerStatusRepository
    {
        Task<ICollection<CustomerStatusDto>> GetAllCustomerStatus();
        Task<CustomerStatusDto> GetCustomerStatus(int id);
        Task<bool> CreateCustomerStatus(CustomerStatusDto CustomerStatus);
        Task<bool> UpdateCustomerStatus(CustomerStatusDto CustomerStatus);
        Task<bool> DeleteCustomerStatus(CustomerStatusDto CustomerStatus);

    }
}
