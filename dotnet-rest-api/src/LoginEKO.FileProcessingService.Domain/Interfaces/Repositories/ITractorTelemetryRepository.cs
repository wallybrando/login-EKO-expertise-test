using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using System.Linq.Expressions;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Repositories
{
    public interface ITractorTelemetryRepository
    {
        Task<IEnumerable<TractorTelemetry>> GetAsync(Expression<Func<TractorTelemetry, bool>> expression, int pageNumber, int pageSize, CancellationToken token = default);
        Task<int> GetCountAsync(Expression<Func<TractorTelemetry, bool>> expression, CancellationToken token = default);
    }
}
