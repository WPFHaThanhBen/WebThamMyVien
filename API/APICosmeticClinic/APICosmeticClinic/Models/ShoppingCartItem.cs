using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class ShoppingCartItem
    {
        public int Id { get; set; }
        public int? ShoppingCartId { get; set; }
        public int? ProductId { get; set; }
        public int? ServiceId { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Service? Service { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }
    }
}
