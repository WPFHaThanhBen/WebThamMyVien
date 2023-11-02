using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IInvoiceDetailRepository
    {
        ICollection<InvoiceDetail> GetAllInvoiceDetail();
        InvoiceDetail GetInvoiceDetail(int id);
		ICollection<InvoiceDetail> GetInvoiceDetailByInvoice(int id);
		bool InvoiceDetailExists(int id);
        bool CreateInvoiceDetail(InvoiceDetail invoiceDetail);
        bool UpdateInvoiceDetail(InvoiceDetail invoiceDetail);
        bool DeleteInvoiceDetail(InvoiceDetail invoiceDetail);
        bool Save();
    }
}
