using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IOrderRepository
    {
        Task<ICollection<OrderDto>> GetAllOrder();
        Task<OrderDto> GetOrder(int id);
        Task<bool> CreateOrder(OrderDto Order);
        Task<bool> UpdateOrder(OrderDto Order);
        Task<bool> DeleteOrder(OrderDto Order);

    }
}
