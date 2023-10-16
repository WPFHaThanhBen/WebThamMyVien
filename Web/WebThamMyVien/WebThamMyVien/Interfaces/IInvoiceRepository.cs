using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IInvoiceRepository
    {
        ICollection<InvoiceDto> GetAllInvoice();
        InvoiceDto GetInvoice(int id);
        bool CreateInvoice(InvoiceDto invoice);
        bool UpdateInvoice(InvoiceDto invoice);
        bool DeleteInvoice(InvoiceDto invoice);
 
    }
}
