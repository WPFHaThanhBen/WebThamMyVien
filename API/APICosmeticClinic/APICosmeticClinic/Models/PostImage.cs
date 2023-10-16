using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class PostImage
    {
        public int Id { get; set; }
        public int? ContentSeqNumber { get; set; }
        public int? PostContentId { get; set; }
        public string? Image { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual PostContent? PostContent { get; set; }
    }
}
