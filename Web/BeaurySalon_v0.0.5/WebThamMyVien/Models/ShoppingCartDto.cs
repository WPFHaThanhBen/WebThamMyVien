using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        [Required]
        public int? UserAccountId { get; set; }
    }
}
