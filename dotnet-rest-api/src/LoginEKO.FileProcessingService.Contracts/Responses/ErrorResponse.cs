using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Contracts.Responses
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string Message { get; private set; } 
        public string Details { get; private set; }

        public ErrorResponse(HttpStatusCode statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}
