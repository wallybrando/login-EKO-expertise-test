using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class CombineTelemetryRepository : ICombineTelemetryRepository
    {
        private readonly ApplicationContext _dbContext;

        private readonly IFilterExpressionBuilder<CombineTelemetry> _filterExpressionBuilder;

        public CombineTelemetryRepository(ApplicationContext dbContext, IFilterExpressionBuilder<CombineTelemetry> filterExpressionBuilder)
        {
            _dbContext = dbContext;
            _filterExpressionBuilder = filterExpressionBuilder;
        }

        public async Task<ICollection<CombineTelemetry>> GetAsync(PaginatedFilter paginatedFilter)
        {
            var query = _dbContext.CombineTelemetries.OrderBy(x => x.Date).AsQueryable();

            var filterExpressions = _filterExpressionBuilder.ApplyFilters(paginatedFilter.Filters);
            foreach (var expression in filterExpressions)
            {
                query = query.Where(expression);
            }

            try
            {
                var records = await query.AsNoTracking()
                    .Skip((paginatedFilter.PageNumber!.Value - 1) * paginatedFilter.PageSize!.Value)
                    .Take(paginatedFilter.PageSize!.Value)
                    .ToListAsync();

                return records;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
