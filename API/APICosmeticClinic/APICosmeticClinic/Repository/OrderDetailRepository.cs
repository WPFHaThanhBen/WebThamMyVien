using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public OrderDetailRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateOrderDetail(OrderDetail orderDetail)
        {
            _context.Add(orderDetail);
            return Save();
        }

        public bool DeleteOrderDetail(OrderDetail orderDetail)
        {
            orderDetail.IsDelete = true;
            orderDetail.DateDelete = sp.GetCurrentDate();
            _context.Update(orderDetail);
            return Save();
        }

        public ICollection<OrderDetail> GetAllOrderDetail()
        {
            return _context.OrderDetails.Where(c => c.IsDelete != true).ToList();
        }

        public ICollection<OrderDetail> GetAllOrderDetailByOrderId(int id)
        {
            return _context.OrderDetails.Where(c => c.IsDelete != true && c.OrderId == id).ToList();
        }

        public OrderDetail GetOrderDetail(int id)
        {
            return _context.OrderDetails.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            _context.Update(orderDetail);
            return Save();
        }
    }
}
