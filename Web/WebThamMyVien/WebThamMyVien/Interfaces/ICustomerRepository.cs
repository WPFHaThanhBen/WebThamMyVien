using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface ICustomerRepository
    {
        Task<ICollection<CustomerDto>> GetAllCustomer();
        Task<CustomerDto> GetCustomer(int id);
        Task<CustomerDto> GetCustomerBySDT(string sdt);
        Task<bool> CreateCustomer(CustomerDto Customer);
        Task<bool> UpdateCustomer(CustomerDto Customer);
        Task<bool> DeleteCustomer(CustomerDto Customer);

    }
}
