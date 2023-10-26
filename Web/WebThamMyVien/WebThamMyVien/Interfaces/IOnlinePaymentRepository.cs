using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IOnlinePaymentRepository
    {
        Task<ICollection<OnlinePaymentDto>> GetAllOnlinePayment();
        Task<OnlinePaymentDto> GetOnlinePayment(int id);
        Task<bool> CreateOnlinePayment(OnlinePaymentDto OnlinePayment);
        Task<bool> UpdateOnlinePayment(OnlinePaymentDto OnlinePayment);
        Task<bool> DeleteOnlinePayment(OnlinePaymentDto OnlinePayment);

    }
}
