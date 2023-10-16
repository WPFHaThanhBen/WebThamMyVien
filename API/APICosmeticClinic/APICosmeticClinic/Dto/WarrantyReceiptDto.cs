namespace APICosmeticClinic.Dto
{
    public class WarrantyReceiptDto
    {

        public int Id { get; set; }
        public string? WarrantyDate { get; set; }
        public int? WarrantyHandledByUserId { get; set; }
        public int? FollowUpUserId { get; set; }
        public string? WarrantyEndDate { get; set; }
        public int? CustomerId { get; set; }
        public int? ServicedServiceId { get; set; }
        public string? WarrantyDetails { get; set; }
        public int? HandledAtBranchId { get; set; }
        public int? WarrantyTypeId { get; set; }
        public string? Other { get; set; }
    }
}
