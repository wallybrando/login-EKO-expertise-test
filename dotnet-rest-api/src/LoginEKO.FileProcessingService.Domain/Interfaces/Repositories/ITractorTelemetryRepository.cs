using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Entities;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Repositories
{
    public interface ITractorTelemetryRepository
    {
        Task<ICollection<TractorTelemetry>> GetAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
        Task<int> GetCountAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
    }
}
