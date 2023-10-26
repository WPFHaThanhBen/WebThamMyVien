using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class InvoiceTypeDto
    {
        public int Id { get; set; }
        [Required]
        public string? InvoiceTypeName { get; set; }
        public string? Other { get; set; }
    }
}
