using ConcertBooking.WebApp.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace ConcertBooking.WebApp.Api.Utilities;

public static class ControllerBaseExtensions
{
    extension(ControllerBase controller)
    {
        /// <summary>
        /// Converts the specified domain <see cref="Error"/> into an <see cref="IActionResult"/> containing problem details with the corresponding HTTP status code.
        /// </summary>
        /// <param name="error">The domain <see cref="Error"/> to convert into a problem response.</param>
        /// <returns>An <see cref="IActionResult"/> containing problem details formatted as <c>application/problem+json</c>.</returns>
        public IActionResult ToProblem(Error error)
        {
            var statusCode = GetStatusCode(error.Type);

            var problemDetails = controller.ProblemDetailsFactory.CreateProblemDetails(
                controller.HttpContext,
                statusCode: statusCode,
                title: error.Code,            // standard title is not needed, we use the error code instead
                detail: statusCode >= 500 ? "An unexpected error occurred." : error.Message,
                instance: controller.HttpContext.Request.Path);

            problemDetails.Extensions["code"] = error.Code;
            problemDetails.Extensions["errorType"] = error.Type.ToString();

            return new ObjectResult(problemDetails)
            {
                StatusCode = statusCode,
                ContentTypes = { "application/problem+json" }
            };
        }

        /// <summary>
        /// Converts the specified operation <see cref="Result"/> into an <see cref="IActionResult"/>, returning a success response when the operation succeeds or a problem response when it fails.
        /// </summary>
        /// <param name="result">The operation result to convert into an HTTP response.</param>
        /// <param name="onSuccess">An optional delegate used to create the success response. If not provided, a no-content response is returned.</param>
        /// <returns>An <see cref="IActionResult"/> representing either the successful outcome or the corresponding problem response.</returns>
        public IActionResult FromResult(Result result, Func<IActionResult>? onSuccess = null) =>
            result.IsSuccess
                ? onSuccess?.Invoke() ?? controller.NoContent() 
                : controller.ToProblem(result.Error);


        /// <summary>
        /// Converts the specified operation result into an <see cref="IActionResult"/>, returning a success response when the result succeeds or a problem response when it fails.
        /// </summary>
        /// <typeparam name="TResult">The type of data contained in the successful result.</typeparam>
        /// <param name="result">The operation result to convert into an HTTP response.</param>
        /// <param name="onSuccess">An optional function used to create the success response from the result data. If not provided, an <see cref="OkObjectResult"/> containing the data is returned.</param>
        /// <returns>An <see cref="IActionResult"/> representing either the successful result data or the corresponding problem response.</returns>
        public IActionResult FromResult<TResult>(Result<TResult> result,
            Func<TResult, IActionResult>? onSuccess = null)
            => result.Match(
                onSuccess ?? (data => controller.Ok(data)), 
                error => controller.ToProblem(error));
    }
    
    private static int GetStatusCode(ErrorType type) => type switch
    {
        ErrorType.Validation          => StatusCodes.Status400BadRequest,
        ErrorType.Unauthorized        => StatusCodes.Status401Unauthorized,
        ErrorType.PaymentRequired     => StatusCodes.Status402PaymentRequired,
        ErrorType.Forbidden           => StatusCodes.Status403Forbidden,
        ErrorType.NotFound            => StatusCodes.Status404NotFound,
        ErrorType.Conflict            => StatusCodes.Status409Conflict,
        ErrorType.InternalServerError => StatusCodes.Status500InternalServerError,
        ErrorType.Failure             => StatusCodes.Status500InternalServerError,
        _                             => StatusCodes.Status500InternalServerError
    };
}