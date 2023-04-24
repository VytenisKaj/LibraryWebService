﻿using Infrastructure.Domains.Books.Models;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Attributes
{
    public class RequiredWhenTakenAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var bookRequest = (BookRequest)validationContext.ObjectInstance;

            if (bookRequest == null)
            {
                return new ValidationResult($"Attribute cannot be used here");
            }

            if (validationContext.MemberName == nameof(bookRequest.ReaderId))
            {
                if(!bookRequest.IsAvailable && bookRequest.ReaderId == null)
                {
                    return new ValidationResult("ReaderId is required when IsAvailable is set to false.");
                }
                return ValidationResult.Success;
            }

            if (validationContext.MemberName == nameof(bookRequest.UnavailableUntil))
            {
                if (!bookRequest.IsAvailable && bookRequest.UnavailableUntil == null)
                {
                    return new ValidationResult("UnavailableUntil is required when IsAvailable is set to false.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
