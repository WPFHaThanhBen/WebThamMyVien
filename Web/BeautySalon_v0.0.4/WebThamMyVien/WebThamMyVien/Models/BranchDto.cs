using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class BranchDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
