using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class InvoiceDetailRepository : IInvoiceDetailRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public InvoiceDetailRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            _context.Add(invoiceDetail);
            return Save();
        }

        public bool DeleteInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            invoiceDetail.IsDelete = true;
            invoiceDetail.DateDelete = sp.GetCurrentDate();
            _context.Update(invoiceDetail);
            return Save();
        }

        public ICollection<InvoiceDetail> GetAllInvoiceDetail()
        {
            return _context.InvoiceDetails.Where(c => c.IsDelete != true).ToList();
        }

        public InvoiceDetail GetInvoiceDetail(int id)
        {
            return _context.InvoiceDetails.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool InvoiceDetailExists(int id)
        {
            return _context.InvoiceDetails.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            _context.Update(invoiceDetail);
            return Save();
        }
		public ICollection<InvoiceDetail> GetInvoiceDetailByInvoice(int id)
		{
			return _context.InvoiceDetails.Where(e => e.InvoiceId == id && e.IsDelete != true).ToList();
		}
	}
}
