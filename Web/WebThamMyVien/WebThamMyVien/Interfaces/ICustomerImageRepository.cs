using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface ICustomerImageRepository
    {
        ICollection<CustomerImageDto> GetAllCustomerImage();
        CustomerImageDto GetCustomerImage(int id);
        bool CreateCustomerImage(CustomerImageDto customerImage);
        bool UpdateCustomerImage(CustomerImageDto customerImage);
        bool DeleteCustomerImage(CustomerImageDto customerImage);
 
    }
}
