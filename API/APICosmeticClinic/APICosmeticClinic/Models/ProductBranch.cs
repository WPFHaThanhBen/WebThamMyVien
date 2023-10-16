using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class ProductBranch
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? BranchId { get; set; }
        public int? Quantity { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual Product? Product { get; set; }
    }
}
