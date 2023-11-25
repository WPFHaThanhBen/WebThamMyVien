using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class OrderStatusDto
    {
        public int Id { get; set; }
        [Required]
        public string? StatusName { get; set; }
    }
}
