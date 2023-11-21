using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IInvoiceRepository
    {
        ICollection<Invoice> GetAllInvoice();
        ICollection<Invoice> GetAllInvoiceByType(int id);
        Invoice GetInvoice(int id);
        Invoice GetInvoiceFinal();
        bool InvoiceExists(int id); 
        bool CreateInvoice(Invoice invoice);
        bool UpdateInvoice(Invoice invoice);
        bool DeleteInvoice(Invoice invoice);
        bool Save();
    }
}
