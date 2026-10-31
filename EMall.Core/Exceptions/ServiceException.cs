namespace EMall.Core.Exceptions
{
    /// <summary>
    ///  Represent default exception that will be 
    ///  thrown from the members of Service layer.
    /// </summary>
    public class ServiceException(string? errorMessage) : Exception
    {
        // The error message.
        private readonly string? _error = errorMessage;
    }
}