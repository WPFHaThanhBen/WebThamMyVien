using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IOnlinePaymentRepository
    {
        ICollection<OnlinePayment> GetAllOnlinePayment();
        OnlinePayment GetOnlinePayment(int id);
        bool OnlinePaymentExists(int id);
        bool CreateOnlinePayment(OnlinePayment onlinePayment);
        bool UpdateOnlinePayment(OnlinePayment onlinePayment);
        bool DeleteOnlinePayment(OnlinePayment onlinePayment);
        bool Save();
    }
}
