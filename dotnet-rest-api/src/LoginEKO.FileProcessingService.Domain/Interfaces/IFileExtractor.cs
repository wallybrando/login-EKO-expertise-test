using LoginEKO.FileProcessingService.Domain.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace LoginEKO.FileProcessingService.Domain.Interfaces
{
    public interface IFileExtractor
    {
        FileType Type { get; init; }
        Task<IEnumerable<string[]>> ExtractDataAsync(IFormFile file, CancellationToken token = default);
    }
}
