namespace EMall.Data.Exceptions
{
    /// <summary>
    ///  Represent exception that will be thrown by the types of EMall.Data project.
    /// </summary>
    internal class DbException(string? errorMessage) : Exception
    {
        /// <summary>
        ///  The error message of the exception.
        /// </summary>
        private readonly string? _error = errorMessage ?? string.Empty;
    }
}