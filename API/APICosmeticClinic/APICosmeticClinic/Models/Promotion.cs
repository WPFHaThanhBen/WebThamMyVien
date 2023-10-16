using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
            OrderDetails = new HashSet<OrderDetail>();
            Products = new HashSet<Product>();
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string? PromotionName { get; set; }
        public string? PromotionValue { get; set; }
        public string? PromotionTime { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
