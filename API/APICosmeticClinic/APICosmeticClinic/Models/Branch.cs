using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Appointments = new HashSet<Appointment>();
            CustomerFollowUps = new HashSet<CustomerFollowUp>();
            CustomerHistories = new HashSet<CustomerHistory>();
            Invoices = new HashSet<Invoice>();
            OnlinePayments = new HashSet<OnlinePayment>();
            Orders = new HashSet<Order>();
            ProductBranches = new HashSet<ProductBranch>();
            Users = new HashSet<User>();
            WarrantyReceipts = new HashSet<WarrantyReceipt>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<CustomerFollowUp> CustomerFollowUps { get; set; }
        public virtual ICollection<CustomerHistory> CustomerHistories { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<OnlinePayment> OnlinePayments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductBranch> ProductBranches { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<WarrantyReceipt> WarrantyReceipts { get; set; }
    }
}
