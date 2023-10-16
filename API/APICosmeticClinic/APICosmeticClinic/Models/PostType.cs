using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class PostType
    {
        public PostType()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string? TypeName { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
