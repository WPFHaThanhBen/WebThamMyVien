using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class User
    {
        public User()
        {
            AppointmentCreatedByUsers = new HashSet<Appointment>();
            AppointmentReceivedByUsers = new HashSet<Appointment>();
            CustomerFollowUps = new HashSet<CustomerFollowUp>();
            CustomerHistoryConsultingUsers = new HashSet<CustomerHistory>();
            CustomerHistoryCustomerCareUsers = new HashSet<CustomerHistory>();
            CustomerHistoryTechnicalUsers = new HashSet<CustomerHistory>();
            Invoices = new HashSet<Invoice>();
            UserAccounts = new HashSet<UserAccount>();
            WarrantyReceiptFollowUpUsers = new HashSet<WarrantyReceipt>();
            WarrantyReceiptWarrantyHandledByUsers = new HashSet<WarrantyReceipt>();
        }

        public int Id { get; set; }
        public string? Idcard { get; set; }
        public string? FullName { get; set; }
        public string? Birthdate { get; set; }
        public string? Gender { get; set; }
        public string? Hometown { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? BankAccount { get; set; }
        public string? CurrentAddress { get; set; }
        public string? HealthInsurance { get; set; }
        public string? SocialInsurance { get; set; }
        public int? Salary { get; set; }
        public int? UserTypeId { get; set; }
        public int? UserStatusId { get; set; }
        public int? TimesPregnant { get; set; }
        public int? BranchId { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual UserStatus? UserStatus { get; set; }
        public virtual UserType? UserType { get; set; }
        public virtual ICollection<Appointment> AppointmentCreatedByUsers { get; set; }
        public virtual ICollection<Appointment> AppointmentReceivedByUsers { get; set; }
        public virtual ICollection<CustomerFollowUp> CustomerFollowUps { get; set; }
        public virtual ICollection<CustomerHistory> CustomerHistoryConsultingUsers { get; set; }
        public virtual ICollection<CustomerHistory> CustomerHistoryCustomerCareUsers { get; set; }
        public virtual ICollection<CustomerHistory> CustomerHistoryTechnicalUsers { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
        public virtual ICollection<WarrantyReceipt> WarrantyReceiptFollowUpUsers { get; set; }
        public virtual ICollection<WarrantyReceipt> WarrantyReceiptWarrantyHandledByUsers { get; set; }
    }
}
