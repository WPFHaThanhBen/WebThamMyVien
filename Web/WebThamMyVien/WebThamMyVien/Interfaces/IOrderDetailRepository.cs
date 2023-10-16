using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IOrderDetailRepository
    {
        ICollection<OrderDetailDto> GetAllOrderDetail();
        OrderDetailDto GetOrderDetail(int id);
        bool CreateOrderDetail(OrderDetailDto orderDetail);
        bool UpdateOrderDetail(OrderDetailDto orderDetail);
        bool DeleteOrderDetail(OrderDetailDto orderDetail);
 
    }
}
