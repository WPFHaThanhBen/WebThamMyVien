using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public InvoiceRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateInvoice(Invoice invoice)
        {
            _context.Add(invoice);
            return Save();
        }

        public bool DeleteInvoice(Invoice invoice)
        {
            invoice.IsDelete = true;
            invoice.DateDelete = sp.GetCurrentDate();
            _context.Update(invoice);
            return Save();
        }

        public ICollection<Invoice> GetAllInvoice()
        {
            return _context.Invoices.Where(c => c.IsDelete != true).ToList();
        }

        public Invoice GetInvoice(int id)
        {
            return _context.Invoices.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateInvoice(Invoice invoice)
        {
            _context.Update(invoice);
            return Save();
        }
    }
}
