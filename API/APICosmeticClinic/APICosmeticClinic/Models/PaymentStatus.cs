using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class PaymentStatus
    {
        public PaymentStatus()
        {
            OnlinePayments = new HashSet<OnlinePayment>();
        }

        public int Id { get; set; }
        public string? StatusName { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<OnlinePayment> OnlinePayments { get; set; }
    }
}
