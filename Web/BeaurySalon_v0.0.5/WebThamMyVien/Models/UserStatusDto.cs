using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class UserStatusDto
    {
        public int Id { get; set; }
        [Required]
        public string? StatusName { get; set; }
        public string? Other { get; set; }
    }
}
