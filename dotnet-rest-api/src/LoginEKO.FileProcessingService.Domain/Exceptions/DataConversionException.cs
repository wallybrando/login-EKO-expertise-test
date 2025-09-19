namespace LoginEKO.FileProcessingService.Domain.Exceptions
{
    public class DataConversionException : Exception
    {
        public DataConversionException() { }

        public DataConversionException(string message) : base(message) { }

        public DataConversionException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
