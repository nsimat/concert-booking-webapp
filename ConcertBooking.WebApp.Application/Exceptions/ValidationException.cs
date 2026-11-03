using FluentValidation.Results;

namespace ConcertBooking.WebApp.Application.Exceptions;

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; set; }

    public ValidationException(ValidationResult validationResult)
    {
        Errors = validationResult.Errors
            .GroupBy(error => error.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.ErrorMessage).ToArray());
    }
}