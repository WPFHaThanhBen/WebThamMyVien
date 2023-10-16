using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class InvoiceTypeRepository : IInvoiceTypeRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public InvoiceTypeRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateInvoiceType(InvoiceType invoiceType)
        {
            _context.Add(invoiceType);
            return Save();
        }

        public bool DeleteInvoiceType(InvoiceType invoiceType)
        {
            invoiceType.IsDelete = true;
            invoiceType.DateDelete = sp.GetCurrentDate();
            _context.Update(invoiceType);
            return Save();
        }

        public ICollection<InvoiceType> GetAllInvoiceType()
        {
            return _context.InvoiceTypes.Where(c => c.IsDelete != true).ToList();
        }

        public InvoiceType GetInvoiceType(int id)
        {
            return _context.InvoiceTypes.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool InvoiceTypeExists(int id)
        {
            return _context.InvoiceTypes.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateInvoiceType(InvoiceType invoiceType)
        {
            _context.Update(invoiceType);
            return Save();
        }
    }
}
