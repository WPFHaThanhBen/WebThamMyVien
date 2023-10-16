using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class InvoiceDetail
    {
        public int Id { get; set; }
        public int SeqNumber { get; set; }
        public int? InvoiceId { get; set; }
        public string? Content { get; set; }
        public int? Quantity { get; set; }
        public int? Discount { get; set; }
        public int? TotalPrice { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Promotion? DiscountNavigation { get; set; }
        public virtual Invoice? Invoice { get; set; }
    }
}
