using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        [Required]
        public int? ProductId { get; set; }
        public string? Image { get; set; }
    }
}
