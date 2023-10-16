using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            Actions = new HashSet<Action>();
            Posts = new HashSet<Post>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int? UserStatusId { get; set; }
        public int? UserId { get; set; }
        public int? AccountTypeId { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual AccountType? AccountType { get; set; }
        public virtual User? User { get; set; }
        public virtual UserStatus? UserStatus { get; set; }
        public virtual ICollection<Action> Actions { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
