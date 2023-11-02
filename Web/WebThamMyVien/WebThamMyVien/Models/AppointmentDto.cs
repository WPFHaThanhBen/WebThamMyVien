using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        [Required]
        public int? CreatedByUserId { get; set; }//
        [Required]
        public int? ReceivedByUserId { get; set; }//
        [Required]
        public string? CreationDate { get; set; }//
        [Required]
        public string? AppointmentDate { get; set; }
        [Required]
        public int? CustomerId { get; set; }//====
        public int? ServicePerformedId { get; set; }//====
        public int? BranchId { get; set; }// mặc định null
        public int? AppointmentStatusId { get; set; }//
        public int? AppointmentTypeId { get; set; }//
        [Required]
        public string? Other { get; set; }
    }
}
