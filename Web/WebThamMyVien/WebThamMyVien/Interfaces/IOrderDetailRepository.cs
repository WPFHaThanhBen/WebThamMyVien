using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<ICollection<OrderDetailDto>> GetAllOrderDetail();
        Task<ICollection<OrderDetailDto>> GetAllOrderDetailByOrderId(int id);
        Task<OrderDetailDto> GetOrderDetail(int id);
        Task<bool> CreateOrderDetail(OrderDetailDto OrderDetail);
        Task<bool> UpdateOrderDetail(OrderDetailDto OrderDetail);
        Task<bool> DeleteOrderDetail(OrderDetailDto OrderDetail);

    }
}
