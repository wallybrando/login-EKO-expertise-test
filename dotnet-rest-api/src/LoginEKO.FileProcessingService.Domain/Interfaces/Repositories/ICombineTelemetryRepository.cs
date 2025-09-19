using LoginEKO.FileProcessingService.Domain.Models.Entities;
using System.Linq.Expressions;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Repositories
{
    public interface ICombineTelemetryRepository
    {
        Task<IEnumerable<CombineTelemetry>> GetAsync(Expression<Func<CombineTelemetry, bool>> expression, int pageNumber, int pageSize, CancellationToken token = default);
        Task<int> GetCountAsync(Expression<Func<CombineTelemetry, bool>> expression, CancellationToken token = default);
    }
}
