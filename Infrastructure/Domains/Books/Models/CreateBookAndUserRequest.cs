using Infrastructure.Attributes;
using Infrastructure.Domains.Users.Models;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Domains.Books.Models
{
    public class CreateBookAndUserRequest
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

        [RequiredWhenTakenCreated]
        public UserCreateRequest? Reader { get; set; } = null;

        [RequiredWhenTakenCreated]
        public DateTime? UnavailableUntil { get; set; } = null;
    }
}
