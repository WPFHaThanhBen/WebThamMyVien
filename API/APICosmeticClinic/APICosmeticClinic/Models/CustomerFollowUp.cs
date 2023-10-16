using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class CustomerFollowUp
    {
        public CustomerFollowUp()
        {
            FollowUpDetails = new HashSet<FollowUpDetail>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? UserId { get; set; }
        public int? FollowUpStatusId { get; set; }
        public int? FollowUpBranchId { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Branch? FollowUpBranch { get; set; }
        public virtual FollowUpStatus? FollowUpStatus { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<FollowUpDetail> FollowUpDetails { get; set; }
    }
}
