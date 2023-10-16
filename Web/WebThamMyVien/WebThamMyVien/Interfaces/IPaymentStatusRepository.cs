using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IPaymentStatusRepository
    {
        ICollection<PaymentStatusDto> GetAllPaymentStatus();
        PaymentStatusDto GetPaymentStatus(int id);
        bool CreatePaymentStatus(PaymentStatusDto paymentStatus);
        bool UpdatePaymentStatus(PaymentStatusDto paymentStatus);
        bool DeletePaymentStatus(PaymentStatusDto paymentStatus);
 
    }
}
