using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class CustomerStatusRepository : ICustomerStatusRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public CustomerStatusRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateCustomerStatus(CustomerStatus customerStatus)
        {
            _context.Add(customerStatus);
            return Save();
        }

        public bool CustomerStatusExists(int id)
        {
            return _context.CustomerStatuses.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool DeleteCustomerStatus(CustomerStatus customerStatus)
        {
            customerStatus.IsDelete = true;
            customerStatus.DateDelete = sp.GetCurrentDate();
            _context.Update(customerStatus);
            return Save();
        }

        public ICollection<CustomerStatus> GetAllCustomerStatus()
        {
            return _context.CustomerStatuses.Where(c => c.IsDelete != true).ToList();
        }

        public CustomerStatus GetCustomerStatus(int id)
        {
            return _context.CustomerStatuses.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCustomerStatus(CustomerStatus customerStatus)
        {
            _context.Update(customerStatus);
            return Save();
        }
    }
}
