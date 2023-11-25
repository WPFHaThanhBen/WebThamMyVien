using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class ProductTypeDto
    {
        public int Id { get; set; }
        [Required]
        public string? ProductTypeName { get; set; }
        public string? Other { get; set; }
    }
}
