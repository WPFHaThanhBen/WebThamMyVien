using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class Action
    {
        public int Id { get; set; }
        public string? Time { get; set; }
        public string? ContentOfChange { get; set; }
        public int? ActionTypeId { get; set; }
        public int? UserId { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ActionType? ActionType { get; set; }
        public virtual UserAccount? User { get; set; }
    }
}
