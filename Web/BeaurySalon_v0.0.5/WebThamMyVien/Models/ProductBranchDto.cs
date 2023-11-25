using System.ComponentModel.DataAnnotations;
namespace WebThamMyVien.Models
{
    public class ProductBranchDto
    {
        public int Id { get; set; }
        [Required]
        public int? ProductId { get; set; }
        public int? BranchId { get; set; }
        public int? Quantity { get; set; }
    }
}
