using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
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
        public Task<bool> InsertTelemetryAsync(Combine telemetry, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
