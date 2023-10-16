using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public CustomerRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateCustomer(Customer customer)
        {
            _context.Add(customer);
            return Save();
        }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool DeleteCustomer(Customer customer)
        {
            customer.IsDelete = true;
            customer.DateDelete = sp.GetCurrentDate();
            _context.Update(customer);
            return Save();
        }

        public ICollection<Customer> GetAllCustomer()
        {
            return _context.Customers.Where(c => c.IsDelete != true).ToList();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCustomer(Customer customer)
        {
            _context.Update(customer);
            return Save();
        }
    }
}
