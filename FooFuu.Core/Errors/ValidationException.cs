namespace FooFuu.Core.Errors;

/// <summary>
/// Custom exception for validation errors.
/// </summary>
public class ValidationException : ApplicationException
{
    public ValidationException(string message) : base(message) { }

    public ValidationException(string message, Exception innerException) 
        : base(message, innerException) { }
}