using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetAllOrder();
        ICollection<Order> GetAllOrderByPhone(string phone);
        Order GetOrder(int id);
        Order GetOrderFinal();
        bool OrderExists(int id);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        bool Save();
    }
}
