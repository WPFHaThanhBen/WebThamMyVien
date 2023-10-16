namespace WebThamMyVien.Models
{
    public class InvoiceDetailDto
    {
        public int Id { get; set; }
        public int SeqNumber { get; set; }
        public int? InvoiceId { get; set; }
        public string? Content { get; set; }
        public int? Quantity { get; set; }
        public int? Discount { get; set; }
        public int? TotalPrice { get; set; }
    }
}
