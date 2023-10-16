using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class Service
    {
        public Service()
        {
            Appointments = new HashSet<Appointment>();
            CustomerHistories = new HashSet<CustomerHistory>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
            WarrantyReceipts = new HashSet<WarrantyReceipt>();
        }

        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public int? ServiceTypeId { get; set; }
        public string? ServiceDetails { get; set; }
        public int? Price { get; set; }
        public string? Other { get; set; }
        public int? FollowUpDays { get; set; }
        public int? AppliedPromotionId { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Promotion? AppliedPromotion { get; set; }
        public virtual ServiceType? ServiceType { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<CustomerHistory> CustomerHistories { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public virtual ICollection<WarrantyReceipt> WarrantyReceipts { get; set; }
    }
}
