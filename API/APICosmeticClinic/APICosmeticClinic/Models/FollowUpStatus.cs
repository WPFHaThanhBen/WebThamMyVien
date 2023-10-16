using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class FollowUpStatus
    {
        public FollowUpStatus()
        {
            CustomerFollowUps = new HashSet<CustomerFollowUp>();
        }

        public int Id { get; set; }
        public string? StatusName { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<CustomerFollowUp> CustomerFollowUps { get; set; }
    }
}
