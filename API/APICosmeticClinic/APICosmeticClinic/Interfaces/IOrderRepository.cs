using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetAllOrder();
        Order GetOrder(int id);
        bool OrderExists(int id);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        bool Save();
    }
}
