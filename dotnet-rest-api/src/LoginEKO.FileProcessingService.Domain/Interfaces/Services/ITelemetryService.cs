using LoginEKO.FileProcessingService.Domain.Models;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Services
{
    public interface ITelemetryService
    {
        Task<UnifiedTelemetry> GetUnifiedTelemetriesAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
    }
}
