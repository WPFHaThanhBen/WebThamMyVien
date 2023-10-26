using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IInvoiceTypeRepository
    {
        Task<ICollection<InvoiceTypeDto>> GetAllInvoiceType();
        Task<InvoiceTypeDto> GetInvoiceType(int id);
        Task<bool> CreateInvoiceType(InvoiceTypeDto InvoiceType);
        Task<bool> UpdateInvoiceType(InvoiceTypeDto InvoiceType);
        Task<bool> DeleteInvoiceType(InvoiceTypeDto InvoiceType);

    }
}
