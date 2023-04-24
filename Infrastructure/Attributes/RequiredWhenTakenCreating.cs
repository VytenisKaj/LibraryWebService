using Infrastructure.Domains.Books.Models;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Attributes
{
    public class RequiredWhenTakenCreatedAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var bookRequest = (CreateBookAndUserRequest)validationContext.ObjectInstance;

            if (bookRequest == null)
            {
                return new ValidationResult($"Attribute cannot be used here");
            }

            if (validationContext.MemberName == nameof(bookRequest.Reader))
            {
                if (bookRequest.Reader == null)
                {
                    return new ValidationResult("Reader is required when IsAvailable is set to false.");
                }
                return ValidationResult.Success;
            }

            if (validationContext.MemberName == nameof(bookRequest.UnavailableUntil))
            {
                if (bookRequest.UnavailableUntil == null)
                {
                    return new ValidationResult("UnavailableUntil is required when IsAvailable is set to false.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
