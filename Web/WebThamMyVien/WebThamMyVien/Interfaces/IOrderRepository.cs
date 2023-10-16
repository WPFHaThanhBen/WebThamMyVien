using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<OrderDto> GetAllOrder();
        OrderDto GetOrder(int id);
        bool CreateOrder(OrderDto order);
        bool UpdateOrder(OrderDto order);
        bool DeleteOrder(OrderDto order);
 
    }
}
