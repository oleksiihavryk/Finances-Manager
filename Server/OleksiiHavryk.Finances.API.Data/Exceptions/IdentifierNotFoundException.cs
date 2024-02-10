namespace OleksiiHavryk.Finances.API.Data.Exceptions;
/// <summary>
///     Exception model for error with unknown identifier in database.
/// </summary>
public class IdentifierNotFoundException : Exception
{
    public const string ErrorMessage = "Unknown identifier for database.";

    public override string Message => base.Message ?? ErrorMessage;

    public IdentifierNotFoundException(string? message = null)  
        : base(message)
    {
    }
    public IdentifierNotFoundException(string? message, Exception? inner = null)
        : base(message, inner)
    {
    }
}