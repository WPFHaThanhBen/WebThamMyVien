using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class CustomerHistoryRepository : ICustomerHistoryRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public CustomerHistoryRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateCustomerHistory(CustomerHistory customerHistory)
        {
            _context.Add(customerHistory);
            return Save();
        }

        public bool CustomerHistoryExists(int id)
        {
            return _context.CustomerHistories.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool DeleteCustomerHistory(CustomerHistory customerHistory)
        {
            customerHistory.IsDelete = true;
            customerHistory.DateDelete = sp.GetCurrentDate();
            _context.Update(customerHistory);
            return Save();
        }

        public ICollection<CustomerHistory> GetAllCustomerHistory()
        {
            return _context.CustomerHistories.Where(c => c.IsDelete != true).ToList();
        }

        public CustomerHistory GetCustomerHistory(int id)
        {
            return _context.CustomerHistories.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCustomerHistory(CustomerHistory customerHistory)
        {
            _context.Update(customerHistory);
            return Save();
        }
    }
}
