using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IInvoiceTypeRepository
    {
        ICollection<InvoiceTypeDto> GetAllInvoiceType();
        InvoiceTypeDto GetInvoiceType(int id);
        bool CreateInvoiceType(InvoiceTypeDto invoiceType);
        bool UpdateInvoiceType(InvoiceTypeDto invoiceType);
        bool DeleteInvoiceType(InvoiceTypeDto invoiceType);
 
    }
}
