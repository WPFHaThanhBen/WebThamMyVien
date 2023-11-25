﻿using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class ServiceTypeDto
    {
        public int Id { get; set; }
        [Required]
        public string? TypeName { get; set; }
        public string? Other { get; set; }
    }
}
