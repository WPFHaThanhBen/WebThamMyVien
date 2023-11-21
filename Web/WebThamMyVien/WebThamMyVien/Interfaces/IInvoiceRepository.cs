using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<ICollection<InvoiceDto>> GetAllInvoice();
        Task<ICollection<InvoiceDto>> GetAllInvoiceByType(int id);
        Task<InvoiceDto> GetInvoice(int id);
        Task<InvoiceDto> GetInvoiceFinal();
        Task<bool> CreateInvoice(InvoiceDto Invoice);
        Task<bool> UpdateInvoice(InvoiceDto Invoice);
        Task<bool> DeleteInvoice(InvoiceDto Invoice);

    }
}
