using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq.Expressions;

namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class CombineTelemetryRepository : ICombineTelemetryRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<CombineTelemetryRepository> _logger;

        public CombineTelemetryRepository(ApplicationContext dbContext, ILogger<CombineTelemetryRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<CombineTelemetry>> GetAsync(Expression<Func<CombineTelemetry, bool>> expression, int pageNumber, int pageSize, CancellationToken token = default)
        {
            _logger.LogDebug("Attempting to get combine telemetry data from database");
            var result = await _dbContext
                .CombineTelemetries
                .OrderBy(x => x.Date)
                .AsQueryable()
                .Where(expression)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);

            _logger.LogDebug("Successfully fetched combine telemetry data from database");
            return result;
        }

        public async Task<int> GetCountAsync(Expression<Func<CombineTelemetry, bool>> expression, CancellationToken token = default)
        {
            _logger.LogDebug("Attempting to get total combine telemetry count from database");
            var result = await _dbContext.CombineTelemetries
                                         .OrderBy(x => x.Date)
                                         .AsQueryable()
                                         .Where(expression)
                                         .CountAsync(token);

            _logger.LogDebug("Successfully fetched total combine telemetry count from database");
            return result;
        }
    }
}
