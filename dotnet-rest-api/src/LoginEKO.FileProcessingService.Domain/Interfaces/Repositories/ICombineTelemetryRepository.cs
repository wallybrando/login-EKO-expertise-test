using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Entities;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Repositories
{
    public interface ICombineTelemetryRepository
    {
        Task<ICollection<CombineTelemetry>> GetAsync(PaginatedFilter filter, CancellationToken token = default);
        Task<int> GetCountAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
    }
}
