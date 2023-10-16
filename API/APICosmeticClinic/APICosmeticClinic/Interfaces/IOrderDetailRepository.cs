using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IOrderDetailRepository
    {
        ICollection<OrderDetail> GetAllOrderDetail();
        OrderDetail GetOrderDetail(int id);
        bool OrderDetailExists(int id);
        bool CreateOrderDetail(OrderDetail orderDetail);
        bool UpdateOrderDetail(OrderDetail orderDetail);
        bool DeleteOrderDetail(OrderDetail orderDetail);
        bool Save();
    }
}
