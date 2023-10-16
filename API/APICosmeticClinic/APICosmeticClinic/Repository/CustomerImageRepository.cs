using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class CustomerImageRepository : ICustomerImageRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public CustomerImageRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateCustomerImage(CustomerImage customerImage)
        {
            _context.Add(customerImage);
            return Save();
        }

        public bool CustomerImageExists(int id)
        {
            return _context.CustomerImages.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool DeleteCustomerImage(CustomerImage customerImage)
        {
            customerImage.IsDelete = true;
            customerImage.DateDelete = sp.GetCurrentDate();
            _context.Update(customerImage);
            return Save();
        }

        public ICollection<CustomerImage> GetAllCustomerImage()
        {
            return _context.CustomerImages.Where(c => c.IsDelete != true).ToList();
        }

        public CustomerImage GetCustomerImage(int id)
        {
            return _context.CustomerImages.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCustomerImage(CustomerImage customerImage)
        {
            _context.Update(customerImage);
            return Save();
        }
    }
}
