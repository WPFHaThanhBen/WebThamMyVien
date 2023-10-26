using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class WarrantyTypeDto
    {
        public int Id { get; set; }
        [Required]
        public string? WarrantyTypeName { get; set; }
        public string? Other { get; set; }
    }
}
