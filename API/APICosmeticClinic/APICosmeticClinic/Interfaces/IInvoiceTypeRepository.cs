using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IInvoiceTypeRepository
    {
        ICollection<InvoiceType> GetAllInvoiceType();
        InvoiceType GetInvoiceType(int id);
        bool InvoiceTypeExists(int id);
        bool CreateInvoiceType(InvoiceType invoiceType);
        bool UpdateInvoiceType(InvoiceType invoiceType);
        bool DeleteInvoiceType(InvoiceType invoiceType);
        bool Save();
    }
}
