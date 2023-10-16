using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class PaymentStatusRepository : IPaymentStatusRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public PaymentStatusRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreatePaymentStatus(PaymentStatus paymentStatus)
        {
            _context.Add(paymentStatus);
            return Save();
        }

        public bool DeletePaymentStatus(PaymentStatus paymentStatus)
        {
            paymentStatus.IsDelete = true;
            paymentStatus.DateDelete = sp.GetCurrentDate();
            _context.Update(paymentStatus);
            return Save();
        }

        public ICollection<PaymentStatus> GetAllPaymentStatus()
        {
            return _context.PaymentStatuses.Where(c => c.IsDelete != true).ToList();
        }

        public PaymentStatus GetPaymentStatus(int id)
        {
            return _context.PaymentStatuses.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool PaymentStatusExists(int id)
        {
            return _context.PaymentStatuses.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePaymentStatus(PaymentStatus paymentStatus)
        {
            _context.Update(paymentStatus);
            return Save();
        }
    }
}
