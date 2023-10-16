using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class WarrantyTypeRepository : IWarrantyTypeRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public WarrantyTypeRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool WarrantyTypeExists(int id)
        {
            return _context.WarrantyTypes.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateWarrantyType(WarrantyType warrantyType)
        {
            _context.Add(warrantyType);
            return Save();
        }

        public bool DeleteWarrantyType(WarrantyType warrantyType)
        {
            warrantyType.IsDelete = true;
            warrantyType.DateDelete = sp.GetCurrentDate();
            _context.Update(warrantyType);
            return Save();
        }

        public ICollection<WarrantyType> GetAllWarrantyType()
        {
            return _context.WarrantyTypes.Where(c => c.IsDelete != true).ToList();
        }

        public WarrantyType GetWarrantyType(int id)
        {
            return _context.WarrantyTypes.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateWarrantyType(WarrantyType warrantyType)
        {
            _context.Update(warrantyType);
            return Save();
        }
    }
}
