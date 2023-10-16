using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class FollowUpDetail
    {
        public int Id { get; set; }
        public int? FollowUpReceiptId { get; set; }
        public string? Details { get; set; }
        public string? FollowUpDate { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual CustomerFollowUp? FollowUpReceipt { get; set; }
    }
}
