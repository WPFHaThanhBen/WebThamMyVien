using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class CustomerImage
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string? Image { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
