namespace APICosmeticClinic.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Birthdate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Idcard { get; set; }
        public string? FacebookLink { get; set; }
        public string? ZaloLink { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? InterestedServices { get; set; }
        public int? CustomerStatusId { get; set; }
        public int? CustomerTypeId { get; set; }
        public string? Other { get; set; }
    }
}
