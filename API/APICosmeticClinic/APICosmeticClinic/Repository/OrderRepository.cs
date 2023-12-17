using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public OrderRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateOrder(Order order)
        {
            _context.Add(order);
            return Save();
        }

        public bool DeleteOrder(Order order)
        {
            order.IsDelete = true;
            order.DateDelete = sp.GetCurrentDate();
            _context.Update(order);
            return Save();
        }

        public ICollection<Order> GetAllOrder()
        {
            return _context.Orders.Where(c => c.IsDelete != true).ToList();
        }

        public ICollection<Order> GetAllOrderByPhone(string phone)
        {
            return _context.Orders.Where(c => c.IsDelete != true && c.RecipientPhoneNumber == phone).ToList();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public Order GetOrderFinal()
        {
            return _context.Orders.Where(e => e.IsDelete != true).OrderByDescending(e => e.Id).FirstOrDefault();
        }

        public bool OrderExists(int id)
        {
            return _context.Orders.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }
    }
}
