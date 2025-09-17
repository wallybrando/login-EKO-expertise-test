using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Entities;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Services
{
    public interface ITractorTelemetryService
    {
        Task<IEnumerable<TractorTelemetry>> GetTractorTelemetriesAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
        Task<int> GetCountAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
    }
}
