using Infrastructure.Domains.Authors.Models;
using Infrastructure.Domains.Users.Models;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Domains.Books.Models
{
    public class BookResponse
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public Author Author { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsAvailable { get; set; } = true;

        public User? Reader { get; set; } = null;

        public DateTime UnavailableUntil { get; set; }

    }
}
