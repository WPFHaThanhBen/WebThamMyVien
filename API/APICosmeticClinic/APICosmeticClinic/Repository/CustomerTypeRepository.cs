using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class CustomerTypeRepository : ICustomerTypeRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public CustomerTypeRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateCustomerType(CustomerType customerType)
        {
            _context.Add(customerType);
            return Save();
        }

        public bool CustomerTypeExists(int id)
        {
            return _context.CustomerTypes.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool DeleteCustomerType(CustomerType customerType)
        {
            customerType.IsDelete = true;
            customerType.DateDelete = sp.GetCurrentDate();
            _context.Update(customerType);
            return Save();
        }

        public ICollection<CustomerType> GetAllCustomerType()
        {
            return _context.CustomerTypes.Where(c => c.IsDelete != true).ToList();
        }

        public CustomerType GetCustomerType(int id)
        {
            return _context.CustomerTypes.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCustomerType(CustomerType customerType)
        {
            _context.Update(customerType);
            return Save();
        }
    }
}
