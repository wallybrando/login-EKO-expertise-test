using System.Net;

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
