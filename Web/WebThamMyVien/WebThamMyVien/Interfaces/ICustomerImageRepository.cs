using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface ICustomerImageRepository
    {
        Task<ICollection<CustomerImageDto>> GetAllCustomerImage();
        Task<ICollection<CustomerImageDto>> GetAllCustomerImageByCustomer(int id);
        Task<CustomerImageDto> GetCustomerImage(int id);
        Task<bool> CreateCustomerImage(CustomerImageDto CustomerImage);
        Task<bool> UpdateCustomerImage(CustomerImageDto CustomerImage);
        Task<bool> DeleteCustomerImage(CustomerImageDto CustomerImage);

    }
}
