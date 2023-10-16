using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class WarrantyReceipt
    {
        public WarrantyReceipt()
        {
            CustomerHistories = new HashSet<CustomerHistory>();
        }

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
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual User? FollowUpUser { get; set; }
        public virtual Branch? HandledAtBranch { get; set; }
        public virtual Service? ServicedService { get; set; }
        public virtual User? WarrantyHandledByUser { get; set; }
        public virtual WarrantyType? WarrantyType { get; set; }
        public virtual ICollection<CustomerHistory> CustomerHistories { get; set; }
    }
}
