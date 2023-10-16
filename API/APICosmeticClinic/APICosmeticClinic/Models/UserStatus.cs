﻿using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            UserAccounts = new HashSet<UserAccount>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? StatusName { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<UserAccount> UserAccounts { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
