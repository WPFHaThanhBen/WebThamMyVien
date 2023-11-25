using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class ActionTypeDto
    {
        public int Id { get; set; }
        [Required]
        public string? TypeName { get; set; }
    }
}
