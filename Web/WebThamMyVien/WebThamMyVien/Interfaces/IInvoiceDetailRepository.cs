using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IInvoiceDetailRepository
    {
        ICollection<InvoiceDetailDto> GetAllInvoiceDetail();
        InvoiceDetailDto GetInvoiceDetail(int id);
        bool CreateInvoiceDetail(InvoiceDetailDto invoiceDetail);
        bool UpdateInvoiceDetail(InvoiceDetailDto invoiceDetail);
        bool DeleteInvoiceDetail(InvoiceDetailDto invoiceDetail);
 
    }
}
