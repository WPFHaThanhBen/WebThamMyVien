using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IOnlinePaymentRepository
    {
        ICollection<OnlinePaymentDto> GetAllOnlinePayment();
        OnlinePaymentDto GetOnlinePayment(int id);
        bool CreateOnlinePayment(OnlinePaymentDto onlinePayment);
        bool UpdateOnlinePayment(OnlinePaymentDto onlinePayment);
        bool DeleteOnlinePayment(OnlinePaymentDto onlinePayment);
 
    }
}
