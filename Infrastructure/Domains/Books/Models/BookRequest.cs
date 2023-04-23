using Infrastructure.Attributes;
using Infrastructure.Domains.Authors.Models;
using System.ComponentModel.DataAnnotations;

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
        public int AuthorId { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsAvailable { get; set; } = true;

        [RequiredWhenTaken]
        public int? ReaderId { get; set; } = null;

        [RequiredWhenTaken]
        public DateTime? UnavailableUntil { get; set; } = null;

    }
}
