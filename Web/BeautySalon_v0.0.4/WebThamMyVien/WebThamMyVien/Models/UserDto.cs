using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string? Idcard { get; set; }
        public string? FullName { get; set; }
        public string? Birthdate { get; set; }
        public string? Gender { get; set; }
        public string? Hometown { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? BankAccount { get; set; }
        public string? CurrentAddress { get; set; }
        public string? HealthInsurance { get; set; }
        public string? SocialInsurance { get; set; }
        public int? Salary { get; set; }
        public int? UserTypeId { get; set; }
        public int? UserStatusId { get; set; }
        public int? TimesPregnant { get; set; }
        public int? BranchId { get; set; }
        public string? Other { get; set; }
    }
}
