using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface ICustomerTypeRepository
    {
        Task<ICollection<CustomerTypeDto>> GetAllCustomerType();
        Task<CustomerTypeDto> GetCustomerType(int id);
        Task<bool> CreateCustomerType(CustomerTypeDto CustomerType);
        Task<bool> UpdateCustomerType(CustomerTypeDto CustomerType);
        Task<bool> DeleteCustomerType(CustomerTypeDto CustomerType);

    }
}
