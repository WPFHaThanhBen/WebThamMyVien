using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? PostingDate { get; set; }
        public int? ViewsCount { get; set; }
        public int? PostTypeId { get; set; }
        public int? PostedByUserId { get; set; }
    }
}
