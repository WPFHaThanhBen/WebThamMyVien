using System.ComponentModel.DataAnnotations;
namespace WebThamMyVien.Models
{
    public class UserTypeDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Other { get; set; }
    }
}
