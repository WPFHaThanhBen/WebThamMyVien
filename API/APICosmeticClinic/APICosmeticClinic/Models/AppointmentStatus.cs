using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class AppointmentStatus
    {
        public AppointmentStatus()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string? StatusName { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
