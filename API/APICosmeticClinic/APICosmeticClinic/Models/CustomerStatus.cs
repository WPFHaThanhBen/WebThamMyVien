using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class CustomerStatus
    {
        public CustomerStatus()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string? StatusName { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
