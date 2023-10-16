using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IOrderStatusRepository
    {
        ICollection<OrderStatus> GetAllOrderStatus();
        OrderStatus GetOrderStatus(int id);
        bool OrderStatusExists(int id);
        bool CreateOrderStatus(OrderStatus orderStatus);
        bool UpdateOrderStatus(OrderStatus orderStatus);
        bool DeleteOrderStatus(OrderStatus orderStatus);
        bool Save();
    }
}
