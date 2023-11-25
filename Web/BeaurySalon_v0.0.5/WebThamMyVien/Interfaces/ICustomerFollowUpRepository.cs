using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface ICustomerFollowUpRepository
    {
        Task<ICollection<CustomerFollowUpDto>> GetAllCustomerFollowUp();
        Task<CustomerFollowUpDto> GetCustomerFollowUp(int id);
        Task<bool> CreateCustomerFollowUp(CustomerFollowUpDto CustomerFollowUp);
        Task<bool> UpdateCustomerFollowUp(CustomerFollowUpDto CustomerFollowUp);
        Task<bool> DeleteCustomerFollowUp(CustomerFollowUpDto CustomerFollowUp);

    }
}
