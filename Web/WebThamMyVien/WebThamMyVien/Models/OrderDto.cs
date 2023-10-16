namespace WebThamMyVien.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int? OrderStatusId { get; set; }
        public string? OrderDate { get; set; }
        public int? BranchId { get; set; }
        public int? CustomerId { get; set; }
        public int? TotalAmount { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? RecipientPhoneNumber { get; set; }
        public string? Other { get; set; }
    }
}
