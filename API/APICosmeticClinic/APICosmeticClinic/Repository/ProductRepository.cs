using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class ProductRepository : IProductRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public ProductRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            return Save();
        }

        public bool DeleteProduct(Product product)
        {
            product.IsDelete = true;
            product.DateDelete = sp.GetCurrentDate();
            _context.Update(product);
            return Save();
        }

        public ICollection<Product> GetAllProduct()
        {
            return _context.Products.Where(c => c.IsDelete != true).ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }
    }
}
