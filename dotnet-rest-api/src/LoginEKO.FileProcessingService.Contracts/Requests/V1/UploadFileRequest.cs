using Microsoft.AspNetCore.Http;

namespace LoginEKO.FileProcessingService.Contracts.Requests.V1
{
    public class UploadFileRequest
    {
        public required IFormFile File { get; init; }
    }
}
