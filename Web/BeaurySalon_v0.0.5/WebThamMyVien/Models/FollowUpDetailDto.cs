using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class FollowUpDetailDto
    {
        public int Id { get; set; }
        [Required]
        public int? FollowUpReceiptId { get; set; }
        public string? Details { get; set; }
        public string? FollowUpDate { get; set; }
    }
}
