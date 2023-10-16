using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface ICustomerFollowUpRepository
    {
        ICollection<CustomerFollowUp> GetAllCustomerFollowUp();
        CustomerFollowUp GetCustomerFollowUp(int id);
        bool CustomerFollowUpExists(int id);
        bool CreateCustomerFollowUp(CustomerFollowUp customerFollowUp);
        bool UpdateCustomerFollowUp(CustomerFollowUp customerFollowUp);
        bool DeleteCustomerFollowUp(CustomerFollowUp customerFollowUp);
        bool Save();
    }
}
