using Microsoft.AspNetCore.Http;

namespace LoginEKO.FileProcessingService.Contracts.Requests
{
    public class UploadFileRequest
    {
        public required IFormFile File { get; init; }
    }
}
