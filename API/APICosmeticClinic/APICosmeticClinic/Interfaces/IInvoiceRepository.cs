using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IInvoiceRepository
    {
        ICollection<Invoice> GetAllInvoice();
        Invoice GetInvoice(int id);
        bool InvoiceExists(int id);
        bool CreateInvoice(Invoice invoice);
        bool UpdateInvoice(Invoice invoice);
        bool DeleteInvoice(Invoice invoice);
        bool Save();
    }
}
