namespace ConcertBooking.WebApp.Domain.Common;

/// <summary>
/// Represents an error with a specific code, message, and type.
/// This class is used to encapsulate error details throughout the application.
/// </summary>
/// <param name="Code">A stable, machine-readable identifier that clients can use to programmatically handle errors.</param>
/// <param name="Message">A human-readable description of the error.</param>
/// <param name="Type">What the HTTP layer reads to pick the right HTTP status code.</param>
public sealed record Error(string Code, string Message, ErrorType Type = ErrorType.Failure)
{
    internal static Error None => new(string.Empty, string.Empty);
    internal static Error Exception => new("Exception", "An error has occured (see. logs");
    internal static Error Null => new("Error.Null", "Null value received!");
    
    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);
    public static Error Validation(string code, string message) => new(code, message, ErrorType.Validation);
    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);
    public static Error Failure(string code, string message) => new(code, message, ErrorType.Failure);
    public static Error Forbidden(string code, string message) => new(code, message, ErrorType.Forbidden);
    public static Error Unauthorized(string code, string message) => new(code, message, ErrorType.Unauthorized);
    public static Error PaymentRequired(string code, string message) => new(code, message, ErrorType.PaymentRequired);
    public static Error InternalServerError(string code, string message) => new(code, message, ErrorType.InternalServerError);
    // others to implement
}

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3,
    Unauthorized = 4,
    PaymentRequired = 5,  
    Forbidden = 6,
    InternalServerError = 7
}