namespace APICosmeticClinic.Dto
{
    public class ActionDto
    {
        public int Id { get; set; }
        public string? Time { get; set; }
        public string? ContentOfChange { get; set; }
        public int? ActionTypeId { get; set; }
        public int? UserId { get; set; }
    }
}
