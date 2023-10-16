using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class Appointment
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
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual AppointmentStatus? AppointmentStatus { get; set; }
        public virtual AppointmentType? AppointmentType { get; set; }
        public virtual Branch? Branch { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual User? ReceivedByUser { get; set; }
        public virtual Service? ServicePerformed { get; set; }
    }
}
