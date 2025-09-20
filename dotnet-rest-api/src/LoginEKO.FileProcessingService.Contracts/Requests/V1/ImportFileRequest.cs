using Microsoft.AspNetCore.Http;

namespace LoginEKO.FileProcessingService.Contracts.Requests.V1
{
    public class ImportFileRequest
    {
        public required IFormFile File { get; init; }
    }
}
