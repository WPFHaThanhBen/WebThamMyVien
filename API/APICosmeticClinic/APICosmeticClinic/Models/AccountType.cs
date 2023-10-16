using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class AccountType
    {
        public AccountType()
        {
            UserAccounts = new HashSet<UserAccount>();
        }

        public int Id { get; set; }
        public string? AccountTypeName { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
