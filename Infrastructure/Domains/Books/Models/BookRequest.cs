﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Domains.Books.Models
{
    public class BookRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string Author { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsAvailable { get; set; } = true;

        public DateTime UnavailableUntil { get; set; }

    }
}
