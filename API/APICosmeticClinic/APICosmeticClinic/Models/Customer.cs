using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Appointments = new HashSet<Appointment>();
            CustomerFollowUps = new HashSet<CustomerFollowUp>();
            CustomerImages = new HashSet<CustomerImage>();
            Invoices = new HashSet<Invoice>();
            Orders = new HashSet<Order>();
            WarrantyReceipts = new HashSet<WarrantyReceipt>();
        }

        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Birthdate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Idcard { get; set; }
        public string? FacebookLink { get; set; }
        public string? ZaloLink { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? InterestedServices { get; set; }
        public int? CustomerStatusId { get; set; }
        public int? CustomerTypeId { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual CustomerStatus? CustomerStatus { get; set; }
        public virtual CustomerType? CustomerType { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<CustomerFollowUp> CustomerFollowUps { get; set; }
        public virtual ICollection<CustomerImage> CustomerImages { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<WarrantyReceipt> WarrantyReceipts { get; set; }
    }
}
