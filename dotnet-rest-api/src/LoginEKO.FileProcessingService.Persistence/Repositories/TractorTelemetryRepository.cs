using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq.Expressions;

namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class TractorTelemetryRepository : ITractorTelemetryRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<TractorTelemetryRepository> _logger;

        public TractorTelemetryRepository(ApplicationContext dbContext, ILogger<TractorTelemetryRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<TractorTelemetry>> GetAsync(Expression<Func<TractorTelemetry, bool>> expression,
                                                                  int pageNumber,
                                                                  int pageSize,
                                                                  CancellationToken token = default)
        {
            var result = await _dbContext.TractorTelemetries
                                         .OrderBy(x => x.Date)
                                         .AsQueryable()
                                         .Where(expression)
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync(token);

            return result;
        }

        public async Task<int> GetCountAsync(Expression<Func<TractorTelemetry, bool>> expression, CancellationToken token = default)
        {
            var result = await _dbContext.TractorTelemetries
                                         .OrderBy(x => x.Date)
                                         .AsQueryable()
                                         .Where(expression)
                                         .CountAsync(token);

            return result;
        }
    }
}
