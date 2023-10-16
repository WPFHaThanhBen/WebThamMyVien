using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class CustomerFollowUpRepository : ICustomerFollowUpRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public CustomerFollowUpRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateCustomerFollowUp(CustomerFollowUp customerFollowUp)
        {
            _context.Add(customerFollowUp);
            return Save();
        }

        public bool CustomerFollowUpExists(int id)
        {
            return _context.CustomerFollowUps.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool DeleteCustomerFollowUp(CustomerFollowUp customerFollowUp)
        {
            customerFollowUp.IsDelete = true;
            customerFollowUp.DateDelete = sp.GetCurrentDate();
            _context.Update(customerFollowUp);
            return Save();
        }

        public ICollection<CustomerFollowUp> GetAllCustomerFollowUp()
        {
            return _context.CustomerFollowUps.Where(c => c.IsDelete != true).ToList();
        }

        public CustomerFollowUp GetCustomerFollowUp(int id)
        {
            return _context.CustomerFollowUps.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; throw new NotImplementedException();
        }

        public bool UpdateCustomerFollowUp(CustomerFollowUp customerFollowUp)
        {
            _context.Update(customerFollowUp);
            return Save();
        }
    }
}
