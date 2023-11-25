using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class ActionDto
    {
        public int Id { get; set; }
        public string? Time { get; set; }
        public string? ContentOfChange { get; set; }
        [Required]
        public int? ActionTypeId { get; set; }
        public int? UserId { get; set; }
    }
}
