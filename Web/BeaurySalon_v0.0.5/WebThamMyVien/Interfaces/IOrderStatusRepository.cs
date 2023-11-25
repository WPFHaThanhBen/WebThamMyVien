using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IOrderStatusRepository
    {
        Task<ICollection<OrderStatusDto>> GetAllOrderStatus();
        Task<OrderStatusDto> GetOrderStatus(int id);
        Task<bool> CreateOrderStatus(OrderStatusDto OrderStatus);
        Task<bool> UpdateOrderStatus(OrderStatusDto OrderStatus);
        Task<bool> DeleteOrderStatus(OrderStatusDto OrderStatus);

    }
}
