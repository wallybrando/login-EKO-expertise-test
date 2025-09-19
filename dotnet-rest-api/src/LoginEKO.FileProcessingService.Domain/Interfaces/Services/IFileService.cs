using LoginEKO.FileProcessingService.Domain.Models.Entities;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Services
{
    public interface IFileService
    {
        Task<int> ImportVehicleTelemetryAsync(FileMetadata file, CancellationToken token = default);
    }
}
