using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class OnlinePaymentDto
    {
        public int Id { get; set; }
        [Required]
        public int? InvoiceId { get; set; }
        public string? PaymentDescription { get; set; }
        public int? PaymentStatusId { get; set; }
        public string? PaymentDate { get; set; }
        public int? AmountPaid { get; set; }
        public int? BranchId { get; set; }
    }
}
