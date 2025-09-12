using LoginEKO.FileProcessingService.Domain.Models;
using System.Data;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Repositories
{
    public interface ICombineTelemetryRepository
    {
        Task<bool> InsertTelemetryAsync(IEnumerable<CombineTelemetry> telemetry, IDbConnection? connection = null, IDbTransaction? transaction = null);
    }
}
