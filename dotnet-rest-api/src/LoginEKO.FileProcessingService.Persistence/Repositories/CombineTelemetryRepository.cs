using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class CombineTelemetryRepository : ICombineTelemetryRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly IFilterExpressionBuilder<CombineTelemetry> _filterExpressionBuilder;
        private readonly ILogger<CombineTelemetryRepository> _logger;

        public CombineTelemetryRepository(ApplicationContext dbContext, IFilterExpressionBuilder<CombineTelemetry> filterExpressionBuilder, ILogger<CombineTelemetryRepository> logger)
        {
            _dbContext = dbContext;
            _filterExpressionBuilder = filterExpressionBuilder;
            _logger = logger;
        }

        public async Task<ICollection<CombineTelemetry>> GetAsync(PaginatedFilter paginatedFilter, CancellationToken token = default)
        {
            _logger.LogTrace("GetAsync() pageNumber={pageNumber} pageSize={pageSize}", paginatedFilter.PageNumber, paginatedFilter.PageSize);
            var query = _dbContext.CombineTelemetries.OrderBy(x => x.Date).AsQueryable();

            var filterExpressions = _filterExpressionBuilder.ApplyFilters(paginatedFilter.Filters);

            try
            {
                _logger.LogDebug("Attempting to get telemetry data from database");
                foreach (var expression in filterExpressions)
                {
                    query = query.Where(expression);
                }

                var records = await query.AsNoTracking()
                    .Skip((paginatedFilter.PageNumber - 1) * paginatedFilter.PageSize)
                    .Take(paginatedFilter.PageSize)
                    .ToListAsync(token);

                _logger.LogDebug("Successfully get telemetry data from database");
                return records;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to get telemetry data. Message: {Message}", ex.Message);
                throw new RepositoryException("Unexpected error occured in database", ex);
            }
        }

        public Task<int> GetCountAsync(PaginatedFilter paginatedFilter, CancellationToken token = default)
        {
            _logger.LogTrace("GetCountAsync() pageNumber={pageNumber} pageSize={pageSize}", paginatedFilter.PageNumber, paginatedFilter.PageSize);
            var query = _dbContext.CombineTelemetries.OrderBy(x => x.Date).AsQueryable();

            var filterExpressions = _filterExpressionBuilder.ApplyFilters(paginatedFilter.Filters);
            try
            {
                _logger.LogDebug("Attempting to get total telemetry data count");
                foreach (var expression in filterExpressions)
                {
                    query = query.Where(expression);
                }

                _logger.LogDebug("Successfully get telemetry data count");
                return query.CountAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to get telemetry count. Message: {Message}", ex.Message);
                throw new RepositoryException("Unexpected error occured in database", ex);
            }
        }
    }
}
