using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class CombineTelemetryRepository : ICombineTelemetryRepository
    {
        private readonly ApplicationContext _dbContext;

        public CombineTelemetryRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> InsertTelemetryAsync(IEnumerable<CombineTelemetry> telemetry, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            try
            {
                await _dbContext.CombineTelemetries.AddRangeAsync(telemetry);
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
