using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class AccountTypeDto
    {
        public int Id { get; set; }
        [Required]
        public string? AccountTypeName { get; set; }
        public string? Other { get; set; }
    }
}
