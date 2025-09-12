using LoginEKO.FileProcessingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Repositories
{
    public interface ITractorTelemetryRepository
    {
        Task<bool> InsertTelemetryAsync(IEnumerable<TractorTelemetry> telemetry, IDbConnection? connection = null, IDbTransaction? transaction = null);
    }
}
