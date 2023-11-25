using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class InvoiceDto
    {
		[Required]
		public int Id { get; set; }
        [Required]
        public int? InvoiceTypeId { get; set; }
		[Required]
		public string? InvoiceDate { get; set; }
		[Required]
		public int? CreatedByUserId { get; set; }
		[Required]
		public int? CustomerId { get; set; }
		[Required]
		public string? PaymentMethod { get; set; }
		[Required]
		public int? TotalAmount { get; set; }
        public int? BranchId { get; set; }
        public string? Other { get; set; }
    }
}
