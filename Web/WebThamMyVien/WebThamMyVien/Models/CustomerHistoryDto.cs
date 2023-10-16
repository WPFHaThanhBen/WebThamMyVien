namespace WebThamMyVien.Models
{
    public class CustomerHistoryDto
    {
        public int Id { get; set; }
        public string? UsageDate { get; set; }
        public int? ServiceId { get; set; }
        public int? TotalAmount { get; set; }
        public int? WarrantyId { get; set; }
        public int? InvoiceId { get; set; }
        public int? ConsultingUserId { get; set; }
        public int? CustomerCareUserId { get; set; }
        public int? TechnicalUserId { get; set; }
        public int? ServiceBranchId { get; set; }
        public string? Other { get; set; }
    }
}
