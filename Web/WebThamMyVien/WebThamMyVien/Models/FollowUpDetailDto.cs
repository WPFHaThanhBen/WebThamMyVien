namespace WebThamMyVien.Models
{
    public class FollowUpDetailDto
    {
        public int Id { get; set; }
        public int? FollowUpReceiptId { get; set; }
        public string? Details { get; set; }
        public string? FollowUpDate { get; set; }
    }
}
