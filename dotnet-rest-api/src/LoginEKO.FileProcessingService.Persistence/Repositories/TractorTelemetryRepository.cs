using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class TractorTelemetryRepository : ITractorTelemetryRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly IFilterExpressionBuilder<TractorTelemetry> _filterExpressionBuilder;
        private readonly ILogger<TractorTelemetryRepository> _logger;

        public TractorTelemetryRepository(ApplicationContext dbContext, IFilterExpressionBuilder<TractorTelemetry> filterExpressionBuilder, ILogger<TractorTelemetryRepository> logger)
        {
            _dbContext = dbContext;
            _filterExpressionBuilder = filterExpressionBuilder;
            _logger = logger;
        }

        /// <summary>Returns paginated tractor telemetry data from database based on applied filter</summary>
        public async Task<ICollection<TractorTelemetry>> GetAsync(PaginatedFilter paginatedFilter, CancellationToken token = default)
        {
            _logger.LogTrace("GetAsync() pageNumber={pageNumber} pageSize={pageSize}", paginatedFilter.PageNumber, paginatedFilter.PageSize);
            var query = _dbContext.TractorTelemetries.OrderBy(x => x.Date).AsQueryable();

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

        /// <summary>Returns total number of affected tractor telemetry data from database based on applied filter</summary>
        public Task<int> GetCountAsync(PaginatedFilter paginatedFilter, CancellationToken token = default)
        {
            _logger.LogTrace("GetCountAsync() pageNumber={pageNumber} pageSize={pageSize}", paginatedFilter.PageNumber, paginatedFilter.PageSize);
            var query = _dbContext.TractorTelemetries.OrderBy(x => x.Date).AsQueryable();

            var filterExpressions = _filterExpressionBuilder.ApplyFilters(paginatedFilter.Filters);
            try
            {
                _logger.LogDebug("Attempting to get total filtered telemetry data count");
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
