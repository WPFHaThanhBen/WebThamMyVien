﻿namespace APICosmeticClinic.Dto
{
    public class PostDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Introduce { get; set; }
        public string? PostingDateCreate { get; set; }
        public string? PostingDateUpdate { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public int? ViewsCount { get; set; }
        public int? PostTypeId { get; set; }
        public int? PostedByUserId { get; set; }
    }
}
