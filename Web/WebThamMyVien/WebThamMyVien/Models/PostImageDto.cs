using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class PostImageDto
    {
        public int Id { get; set; }
        [Required]
        public int? ContentSeqNumber { get; set; }
        public int? PostContentID { get; set; }
        public string? Image { get; set; }
    }
}
