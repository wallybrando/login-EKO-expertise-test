using LoginEKO.FileProcessingService.Domain.Models;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Services
{
    public interface IFileService
    {
        Task<int> ImportVehicleTelemetryAsync(FileMetadata file, CancellationToken token = default);
    }
}
