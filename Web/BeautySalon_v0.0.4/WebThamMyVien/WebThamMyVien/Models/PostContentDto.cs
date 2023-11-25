using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class PostContentDto
    {
        public int Id { get; set; }
        [Required]
        public int SeqNumber { get; set; }
        public int PostId { get; set; }
        public string? Content { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
    }
}
