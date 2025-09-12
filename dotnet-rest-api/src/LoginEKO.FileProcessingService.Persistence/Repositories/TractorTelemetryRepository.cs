using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Persistence.Database;
using LoginEKO.FileProcessingService.Persistence.DML;
using Microsoft.AspNetCore.Http.Features;
using System.Data;
using Z.Dapper.Plus;

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



            int result;
            if (connection != null)
            {
                if (transaction != null)
                {
                    try
                    {
                        await connection.UseBulkOptions(options => options.InsertIfNotExists = true).BulkInsertAsync("Customer_KeepIdentity", telemetry);
                        return true;
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
            }
            throw new NotImplementedException();
        }
    }
}
