using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        [Required]
        public int? InvoiceTypeId { get; set; }
        public string? InvoiceDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? CustomerId { get; set; }
        public string? PaymentMethod { get; set; }
        public int? TotalAmount { get; set; }
        public int? BranchId { get; set; }
        public string? Other { get; set; }
    }
}
