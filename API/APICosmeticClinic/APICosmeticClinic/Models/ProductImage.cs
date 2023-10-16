using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? Image { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Product? Product { get; set; }
    }
}
