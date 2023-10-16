using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int SeqNumber { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? PromotionId { get; set; }
        public int? TotalPrice { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Promotion? Promotion { get; set; }
    }
}
