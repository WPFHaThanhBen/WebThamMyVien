using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public ProductTypeRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateProductType(ProductType productType)
        {
            _context.Add(productType);
            return Save();
        }

        public bool DeleteProductType(ProductType productType)
        {
            productType.IsDelete = true;
            productType.DateDelete = sp.GetCurrentDate();
            _context.Update(productType);
            return Save();
        }

        public ICollection<ProductType> GetAllProductType()
        {
            return _context.ProductTypes.Where(c => c.IsDelete != true).ToList();
        }

        public ProductType GetProductType(int id)
        {
            return _context.ProductTypes.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool ProductTypeExists(int id)
        {
            return _context.ProductTypes.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProductType(ProductType productType)
        {
            _context.Update(productType);
            return Save();
        }
    }
}
