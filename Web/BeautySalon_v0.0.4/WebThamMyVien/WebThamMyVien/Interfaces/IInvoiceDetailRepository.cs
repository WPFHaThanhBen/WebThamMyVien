using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IInvoiceDetailRepository
    {
        Task<ICollection<InvoiceDetailDto>> GetAllInvoiceDetail();
        Task<InvoiceDetailDto> GetInvoiceDetail(int id);
        Task<bool> CreateInvoiceDetail(InvoiceDetailDto InvoiceDetail);
        Task<bool> UpdateInvoiceDetail(InvoiceDetailDto InvoiceDetail);
        Task<bool> DeleteInvoiceDetail(InvoiceDetailDto InvoiceDetail);
 
    }
}
