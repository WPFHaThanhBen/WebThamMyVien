using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class OnlinePayment
    {
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public string? PaymentDescription { get; set; }
        public int? PaymentStatusId { get; set; }
        public string? PaymentDate { get; set; }
        public int? AmountPaid { get; set; }
        public int? BranchId { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual Invoice? Invoice { get; set; }
        public virtual PaymentStatus? PaymentStatus { get; set; }
    }
}
