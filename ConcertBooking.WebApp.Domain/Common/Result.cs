namespace ConcertBooking.WebApp.Domain.Common;

/// <summary>
/// Represents the result of an operation, encapsulating both success and failure outcomes.
/// </summary>
public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    private Result(bool isSuccess, Error error)
    {
        if(isSuccess && error is not null)
            throw new InvalidOperationException("A successful result cannot have an error!");
        
        if(!isSuccess && error is null)
            throw new InvalidOperationException("A failed result must have an error!");

        IsSuccess = isSuccess;
        Error = error ?? throw new ArgumentNullException(nameof(error));
    }

    /// <summary>
    /// Creates a successful result indicating that an operation completed successfully.
    /// </summary>
    /// <returns>
    /// A <see cref="Result"/> instance representing a successful operation with no associated error.
    /// </returns>
    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    /// <summary>
    /// Creates a failed result indicating that an operation did not succeed due to a specific error.
    /// </summary>
    /// <param name="error">The <see cref="Error"/> object containing the details of the failure.</param>
    /// <returns>
    /// A <see cref="Result"/> instance representing a failure with the provided error details.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided error has an invalid code or an invalid message.
    /// </exception>
    public static Result Failure(Error error)
    {
        if (string.IsNullOrEmpty(error.Code))
            throw new ArgumentException("Error code invalid!", nameof(error));
        
        if(string.IsNullOrWhiteSpace(error.Message))
            throw new ArgumentException("Error message invalid!", nameof(error));

        return new Result(false, error);
    }

    /// <summary>
    /// Defines an implicit conversion from an <see cref="Error"/> to a <see cref="Result"/>.
    /// </summary>
    /// <param name="error">The <see cref="Error"/> instance to be converted into a failure result.</param>
    /// <returns>A <see cref="Result"/> representing the failure outcome associated with the given <see cref="Error"/>.</returns>
    public static implicit operator Result(Error error)
    {
        return Failure(error);
    }

    /// <summary>
    /// Defines an implicit conversion from an <see cref="Exception"/> to a <see cref="Result"/>.
    /// </summary>
    /// <param name="exception">The <see cref="Exception"/> instance to be converted.</param>
    /// <returns>
    /// A <see cref="Result"/> instance representing a failed operation with an error based on the provided exception.
    /// </returns>
    public static implicit operator Result(Exception exception)
    {
        return Failure(Error.Exception);
    }
}

/// <summary>
/// Represents the result of an operation, encapsulating the success or failure state and an optional error.
/// </summary>
public class Result<TResult>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public TResult Data { get; }

    private Result(bool isSuccess, Error error, TResult data = default!)
    {
        IsSuccess = isSuccess;
        Error = error ?? throw new ArgumentNullException(nameof(error));
        Data = data;
    }

    /// <summary>
    /// Creates a successful result encapsulating the given data.
    /// </summary>
    /// <param name="data">The data to encapsulate in the success result. Must not be null.</param>
    /// <returns>
    /// A <see cref="Result{TResult}"/> representing a successful operation containing the provided data,
    /// or a failure result if the data is null.
    /// </returns>
    public static Result<TResult> Success(TResult data)
    {
        return data is null
            ? new Result<TResult>(false, Error.Null) 
            : new Result<TResult>(true, Error.None, data);
    }

    /// <summary>
    /// Creates a failure result encapsulating the given error.
    /// </summary>
    /// <param name="error">The error to encapsulate in the failure result. Must not have an empty code or null/whitespace message.</param>
    /// <returns>
    /// A <see cref="Result{TResult}"/> representing a failed operation containing the provided error.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the error code is empty, or the error message is null or whitespace.</exception>
    public static Result<TResult> Failure(Error error)
    {
        if (string.IsNullOrEmpty(error.Code))
            throw new ArgumentException("Error code invalid!", nameof(error));

        return string.IsNullOrWhiteSpace(error.Message) 
            ? throw new ArgumentException("Error message invalid!", nameof(error)) 
            : new Result<TResult>(false, error);
    }

    /// <summary>
    /// Converts an <see cref="Error"/> instance into a <see cref="Result{TResult}"/> instance
    /// representing a failure result with the provided error.
    /// </summary>
    /// <param name="error">The error to encapsulate in the failure result. Must not be null or contain invalid values.</param>
    /// <returns>
    /// A failure <see cref="Result{TResult}"/> instance encapsulating the given error.
    /// </returns>
    public static implicit operator Result<TResult>(Error error)
    {
        return Failure(error);
    }

    /// <summary>
    /// Allows an implicit conversion from an <see cref="Exception"/> to a failure <see cref="Result{TResult}"/>
    /// with a predefined error value for exceptions.
    /// </summary>
    /// <param name="exception">The exception to convert into a failure result. Cannot be null.</param>
    /// <returns>
    /// A <see cref="Result{TResult}"/> representing a failed operation, with the associated error set to a predefined exception error.
    /// </returns>
    public static implicit operator Result<TResult>(Exception exception)
    {
        return Failure(Error.Exception);
    }

    /// <summary>
    /// Implicitly converts a data instance of type <typeparamref name="TResult"/> into a successful <see cref="Result{TResult}"/>.
    /// </summary>
    /// <param name="data">The data to encapsulate in the success result. Must not be null.</param>
    /// <returns>
    /// A <see cref="Result{TResult}"/> instance representing a successful operation containing the provided data,
    /// or a failure result if the data is null.
    /// </returns>
    public static implicit operator Result<TResult>(TResult data)
    {
        return Success(data);
    }
    
    /// <summary>
    /// Transforms the result of an operation into a specified output type based on its success or failure.
    /// </summary>
    /// <typeparam name="TOut">The type of the output produced by the transformation functions.</typeparam>
    /// <param name="onSuccess">The function to apply when the operation is successful. Receives the data of type <typeparamref name="TResult"/>.</param>
    /// <param name="onFailure">The function to apply when the operation fails. Receives the error of type <see cref="Error"/>.</param>
    /// <returns>
    /// The transformed value of type <typeparamref name="TOut"/>, as determined by either the success or failure function.
    /// </returns>
    public TOut Match<TOut>(Func<TResult, TOut> onSuccess, Func<Error, TOut> onFailure)
    {
        return IsSuccess ? onSuccess(Data) : onFailure(Error);
    }
}