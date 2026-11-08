using ConcertBooking.WebApp.Domain.Common;
using FluentValidation;
using MediatR;

namespace ConcertBooking.WebApp.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
     private readonly IEnumerable<IValidator<TRequest>> _validators;

     public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
     {
          _validators = validators;
     }
     
     public async Task<TResponse> Handle(
          TRequest request, 
          RequestHandlerDelegate<TResponse> next, 
          CancellationToken cancellationToken)
     {
          // Step 1: If there is no validator for this request, go directly to the next operation
          if (!_validators.Any())
               return await next(cancellationToken);
          
          // Step 2: Execute FluentValidation validation
          var context = new ValidationContext<TRequest>(request);
          var validationResults =
               await Task.WhenAll(
                    _validators
                         .Select(v => v.ValidateAsync(context, cancellationToken)));

          var failures = validationResults
               .SelectMany(r => r.Errors)
               .Where(f => f is not null)
               .ToList();
          
          // Step 3: If errors has been found, dynamically create a Result.Failure object
          if (failures.Count != 0)
          {
               // Concatenation of errors messages
               var errorDetails = string.Join(" | ", failures.Select(f => f.ErrorMessage));
               
               // Create a record Error with type "ErrorType.Validation"
               var validationError = Error.Validation($"{typeof(TRequest).Name}.Validation", errorDetails);

               return (TResponse)(dynamic)validationError;
          }
          
          // Step 4: If all is valid, go to the handler
          return await next(cancellationToken);
     }
}