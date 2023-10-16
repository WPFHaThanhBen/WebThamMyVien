using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class ActionType
    {
        public ActionType()
        {
            Actions = new HashSet<Action>();
        }

        public int Id { get; set; }
        public string? TypeName { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<Action> Actions { get; set; }
    }
}
