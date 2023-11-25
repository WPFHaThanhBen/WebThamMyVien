using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class UserAccountDto
    {
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int? UserStatusId { get; set; }
        public int? UserId { get; set; }
        public int? AccountTypeId { get; set; }
    }
}
