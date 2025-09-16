using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Exceptions
{
    public class FileValidationException : Exception
    {
        public FileValidationException() { }

        public FileValidationException(string message) : base(message) { }

        public FileValidationException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
