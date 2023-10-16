using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class WarrantyType
    {
        public WarrantyType()
        {
            WarrantyReceipts = new HashSet<WarrantyReceipt>();
        }

        public int Id { get; set; }
        public string? WarrantyTypeName { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<WarrantyReceipt> WarrantyReceipts { get; set; }
    }
}
