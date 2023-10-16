using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int? OrderStatusId { get; set; }
        public string? OrderDate { get; set; }
        public int? BranchId { get; set; }
        public int? CustomerId { get; set; }
        public int? TotalAmount { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? RecipientPhoneNumber { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual OrderStatus? OrderStatus { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
