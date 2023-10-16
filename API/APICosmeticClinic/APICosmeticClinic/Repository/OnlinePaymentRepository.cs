using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class OnlinePaymentRepository : IOnlinePaymentRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public OnlinePaymentRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateOnlinePayment(OnlinePayment onlinePayment)
        {
            _context.Add(onlinePayment);
            return Save();
        }

        public bool DeleteOnlinePayment(OnlinePayment onlinePayment)
        {
            onlinePayment.IsDelete = true;
            onlinePayment.DateDelete = sp.GetCurrentDate();
            _context.Update(onlinePayment);
            return Save();
        }

        public ICollection<OnlinePayment> GetAllOnlinePayment()
        {
            return _context.OnlinePayments.Where(c => c.IsDelete != true).ToList();
        }

        public OnlinePayment GetOnlinePayment(int id)
        {
            return _context.OnlinePayments.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool OnlinePaymentExists(int id)
        {
            return _context.OnlinePayments.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOnlinePayment(OnlinePayment onlinePayment)
        {
            _context.Update(onlinePayment);
            return Save();
        }
    }
}
