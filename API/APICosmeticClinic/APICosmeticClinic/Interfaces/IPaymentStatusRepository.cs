using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IPaymentStatusRepository
    {
        ICollection<PaymentStatus> GetAllPaymentStatus();
        PaymentStatus GetPaymentStatus(int id);
        bool PaymentStatusExists(int id);
        bool CreatePaymentStatus(PaymentStatus paymentStatus);
        bool UpdatePaymentStatus(PaymentStatus paymentStatus);
        bool DeletePaymentStatus(PaymentStatus paymentStatus);
        bool Save();
    }
}
