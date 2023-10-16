using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IOrderStatusRepository
    {
        ICollection<OrderStatusDto> GetAllOrderStatus();
        OrderStatusDto GetOrderStatus(int id);
        bool CreateOrderStatus(OrderStatusDto orderStatus);
        bool UpdateOrderStatus(OrderStatusDto orderStatus);
        bool DeleteOrderStatus(OrderStatusDto orderStatus);
 
    }
}
