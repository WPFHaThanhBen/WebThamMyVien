using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class PromotionDto
    {
        public int Id { get; set; }
        [Required]
        public string PromotionName { get; set; }
        public string PromotionValue { get; set; }
        public string PromotionTime { get; set; }
        public string Other { get; set; }
    }
}
