using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class InvoiceType
    {
        public InvoiceType()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string? InvoiceTypeName { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
