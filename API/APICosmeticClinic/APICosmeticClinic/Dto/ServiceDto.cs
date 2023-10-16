namespace APICosmeticClinic.Dto
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public int? ServiceTypeId { get; set; }
        public string? ServiceDetails { get; set; }
        public int? Price { get; set; }
        public string? Other { get; set; }
        public int? FollowUpDays { get; set; }
        public int? AppliedPromotionId { get; set; }
    }
}
