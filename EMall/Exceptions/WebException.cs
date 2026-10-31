namespace EMall.Exceptions
{
    /// <summary>
    ///  Exception that will be thrown by the members of the WebMVC layer.
    /// </summary>
    public class WebException(string? errorMessage) : Exception
    {
        // Default error message.
        private readonly string error = errorMessage ?? string.Empty;
    }
}