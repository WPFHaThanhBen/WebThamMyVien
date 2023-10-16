namespace WebThamMyVien.Models
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int SeqNumber { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? PromotionId { get; set; }
        public int? TotalPrice { get; set; }
    }
}
