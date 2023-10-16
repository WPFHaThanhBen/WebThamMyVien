using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class WarrantyReceiptRepository : IWarrantyReceiptRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public WarrantyReceiptRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool WarrantyReceiptExists(int id)
        {
            return _context.WarrantyReceipts.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateWarrantyReceipt(WarrantyReceipt warrantyReceipt)
        {
            _context.Add(warrantyReceipt);
            return Save();
        }

        public bool DeleteWarrantyReceipt(WarrantyReceipt warrantyReceipt)
        {
            warrantyReceipt.IsDelete = true;
            warrantyReceipt.DateDelete = sp.GetCurrentDate();
            _context.Update(warrantyReceipt);
            return Save();
        }

        public ICollection<WarrantyReceipt> GetAllWarrantyReceipt()
        {
            return _context.WarrantyReceipts.Where(c => c.IsDelete != true).ToList();
        }

        public WarrantyReceipt GetWarrantyReceipt(int id)
        {
            return _context.WarrantyReceipts.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateWarrantyReceipt(WarrantyReceipt warrantyReceipt)
        {
            _context.Update(warrantyReceipt);
            return Save();
        }
    }
}
