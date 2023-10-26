using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IPaymentStatusRepository
    {
        Task<ICollection<PaymentStatusDto>> GetAllPaymentStatus();
        Task<PaymentStatusDto> GetPaymentStatus(int id);
        Task<bool> CreatePaymentStatus(PaymentStatusDto PaymentStatus);
        Task<bool> UpdatePaymentStatus(PaymentStatusDto PaymentStatus);
        Task<bool> DeletePaymentStatus(PaymentStatusDto PaymentStatus);

    }
}
