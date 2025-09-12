using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Persistence.Database;
using System.Data;
namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class TractorTelemetryRepository : ITractorTelemetryRepository
    {
        private readonly ApplicationContext _dbContext;
        public TractorTelemetryRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> InsertTelemetryAsync(IEnumerable<TractorTelemetry> telemetry, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            try
            {
                await _dbContext.TractorTelemetries.AddRangeAsync(telemetry);
                var affectedRows = await _dbContext.SaveChangesAsync();

                return affectedRows > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
