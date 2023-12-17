namespace WebThamMyVien.Models
{
    public class OrderView:OrderDto
    {
        public string orderStatusName { get; set; }
        public bool dathanhtoan { get; set; }
        public List<OrderDetailView> OrderDetails { get; set; }
    }
}
