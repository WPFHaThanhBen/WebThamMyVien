namespace APICosmeticClinic.Dto
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ReceivedByUserId { get; set; }
        public string? CreationDate { get; set; }
        public string? AppointmentDate { get; set; }
        public int? CustomerId { get; set; }
        public int? ServicePerformedId { get; set; }
        public int? BranchId { get; set; }
        public int? AppointmentStatusId { get; set; }
        public int? AppointmentTypeId { get; set; }
        public string? Other { get; set; }
    }
}
