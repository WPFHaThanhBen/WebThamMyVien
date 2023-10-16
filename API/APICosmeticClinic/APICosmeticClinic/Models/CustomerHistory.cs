using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class CustomerHistory
    {
        public int Id { get; set; }
        public string? UsageDate { get; set; }
        public int? ServiceId { get; set; }
        public int? TotalAmount { get; set; }
        public int? WarrantyId { get; set; }
        public int? InvoiceId { get; set; }
        public int? ConsultingUserId { get; set; }
        public int? CustomerCareUserId { get; set; }
        public int? TechnicalUserId { get; set; }
        public int? ServiceBranchId { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual User? ConsultingUser { get; set; }
        public virtual User? CustomerCareUser { get; set; }
        public virtual Invoice? Invoice { get; set; }
        public virtual Service? Service { get; set; }
        public virtual Branch? ServiceBranch { get; set; }
        public virtual User? TechnicalUser { get; set; }
        public virtual WarrantyReceipt? Warranty { get; set; }
    }
}
