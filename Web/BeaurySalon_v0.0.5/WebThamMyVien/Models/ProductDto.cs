using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string? ProductName { get; set; }
        public string? Introduction { get; set; }
        public string? Functionality { get; set; }
        public string? Origin { get; set; }
        public int? SellingPrice { get; set; }
        public int? PurchasePrice { get; set; }
        public int? AppliedPromotionId { get; set; }
        public int? ProductTypeId { get; set; }
        public string? Other { get; set; }
    }
}
