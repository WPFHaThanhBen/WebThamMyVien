using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface ICustomerFollowUpRepository
    {
        ICollection<CustomerFollowUpDto> GetAllCustomerFollowUp();
        CustomerFollowUpDto GetCustomerFollowUp(int id);
        bool CreateCustomerFollowUp(CustomerFollowUpDto customerFollowUp);
        bool UpdateCustomerFollowUp(CustomerFollowUpDto customerFollowUp);
        bool DeleteCustomerFollowUp(CustomerFollowUpDto customerFollowUp);
 
    }
}
