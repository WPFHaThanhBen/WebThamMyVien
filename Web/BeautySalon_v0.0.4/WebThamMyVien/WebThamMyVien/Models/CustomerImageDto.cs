using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class CustomerImageDto
    {
        public int Id { get; set; }
        [Required]
        public int? CustomerId { get; set; }
        public string? Image { get; set; }
    }
}
