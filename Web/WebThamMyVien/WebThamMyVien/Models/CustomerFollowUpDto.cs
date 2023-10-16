namespace WebThamMyVien.Models
{
    public class CustomerFollowUpDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? UserId { get; set; }
        public int? FollowUpStatusId { get; set; }
        public int? FollowUpBranchId { get; set; }
    }
}
