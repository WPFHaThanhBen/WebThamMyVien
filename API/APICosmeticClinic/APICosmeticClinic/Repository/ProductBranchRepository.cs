using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class ProductBranchRepository : IProductBranchRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public ProductBranchRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateProductBranch(ProductBranch productBranch)
        {
            _context.Add(productBranch);
            return Save();
        }

        public bool DeleteProductBranch(ProductBranch productBranch)
        {
            productBranch.IsDelete = true;
            productBranch.DateDelete = sp.GetCurrentDate();
            _context.Update(productBranch);
            return Save();
        }

        public ICollection<ProductBranch> GetAllProductBranch()
        {
            return _context.ProductBranches.Where(c => c.IsDelete != true).ToList();
        }

        public ProductBranch GetProductBranch(int id)
        {
            return _context.ProductBranches.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool ProductBranchExists(int id)
        {
            return _context.ProductBranches.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProductBranch(ProductBranch productBranch)
        {
            _context.Update(productBranch);
            return Save();
        }
    }
}
