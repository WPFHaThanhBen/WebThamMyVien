using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public ProductImageRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateProductImage(ProductImage productImage)
        {
            _context.Add(productImage);
            return Save();
        }

        public bool DeleteProductImage(ProductImage productImage)
        {
            productImage.IsDelete = true;
            productImage.DateDelete = sp.GetCurrentDate();
            _context.Update(productImage);
            return Save();
        }

        public ICollection<ProductImage> GetAllProductImage()
        {
            return _context.ProductImages.Where(c => c.IsDelete != true).ToList();
        }

        public ProductImage GetProductImage(int id)
        {
            return _context.ProductImages.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool ProductImageExists(int id)
        {
            return _context.ProductImages.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProductImage(ProductImage productImage)
        {
            _context.Update(productImage);
            return Save();
        }
    }
}
