using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Exceptions
{
    public class FilterValidationException : Exception
    {
        public FilterValidationException() { }

        public FilterValidationException(string message) : base(message) { }

        public FilterValidationException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
