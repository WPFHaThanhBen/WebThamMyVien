using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            CustomerHistories = new HashSet<CustomerHistory>();
            InvoiceDetails = new HashSet<InvoiceDetail>();
            OnlinePayments = new HashSet<OnlinePayment>();
        }

        public int Id { get; set; }
        public int? InvoiceTypeId { get; set; }
        public string? InvoiceDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? CustomerId { get; set; }
        public string? PaymentMethod { get; set; }
        public int? TotalAmount { get; set; }
        public int? BranchId { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }
        public string? Other { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual InvoiceType? InvoiceType { get; set; }
        public virtual ICollection<CustomerHistory> CustomerHistories { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<OnlinePayment> OnlinePayments { get; set; }
    }
}
