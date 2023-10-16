using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? StatusName { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
