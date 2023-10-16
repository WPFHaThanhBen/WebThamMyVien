using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public OrderStatusRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateOrderStatus(OrderStatus orderStatus)
        {
            _context.Add(orderStatus);
            return Save();
        }

        public bool DeleteOrderStatus(OrderStatus orderStatus)
        {
            orderStatus.IsDelete = true;
            orderStatus.DateDelete = sp.GetCurrentDate();
            _context.Update(orderStatus);
            return Save();
        }

        public ICollection<OrderStatus> GetAllOrderStatus()
        {
            return _context.OrderStatuses.Where(c => c.IsDelete != true).ToList();
        }

        public OrderStatus GetOrderStatus(int id)
        {
            return _context.OrderStatuses.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool OrderStatusExists(int id)
        {
            return _context.OrderStatuses.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOrderStatus(OrderStatus orderStatus)
        {
            _context.Update(orderStatus);
            return Save();
        }
    }
}
