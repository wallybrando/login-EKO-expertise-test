using LoginEKO.FileProcessingService.Domain.Models;
using System.Data;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Repositories
{
    public interface ICombineTelemetryRepository
    {
        Task<ICollection<CombineTelemetry>> GetAsync(PaginatedFilter paginatedFilter);
    }
}
