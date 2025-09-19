using LoginEKO.FileProcessingService.Domain.Models;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Services
{
    public interface ITelemetryService
    {
        Task<UnifiedTelemetry> GetTractorTelemetriesAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
    }
}
