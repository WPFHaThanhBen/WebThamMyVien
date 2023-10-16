using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class PostContent
    {
        public PostContent()
        {
            PostImages = new HashSet<PostImage>();
        }

        public int Id { get; set; }
        public int SeqNumber { get; set; }
        public int PostId { get; set; }
        public string? Content { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual ICollection<PostImage> PostImages { get; set; }
    }
}
