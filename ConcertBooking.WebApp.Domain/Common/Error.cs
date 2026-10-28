namespace ConcertBooking.WebApp.Domain.Common;

/// <summary>
/// Represents an error with a specific code, message, and type.
/// This class is used to encapsulate error details throughout the application.
/// </summary>
public sealed record Error(string Code, string Message, ErrorType Type = ErrorType.Failure)
{
    internal static Error None => new("", "");
    internal static Error Exception => new("Exception", "An error has occured (see. logs");
    internal static Error Null => new Error("Error.Null", "Null value received!");
    
    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);
    public static Error Validation(string code, string message) => new(code, message, ErrorType.Validation);
    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);
    public static Error Failure(string code, string message) => new(code, message, ErrorType.Failure);
}

public enum ErrorType
{
    Failure,
    Validation,
    NotFound,
    Conflict
}