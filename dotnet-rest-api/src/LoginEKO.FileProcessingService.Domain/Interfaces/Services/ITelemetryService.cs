using LoginEKO.FileProcessingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Services
{
    public interface ITelemetryService
    {
        Task<IEnumerable<TractorTelemetry>> GetTractorTelemetriesAsync(PaginatedFilter paginatedFilter);
        Task<IEnumerable<CombineTelemetry>> GetCombineTelemetriesAsync(PaginatedFilter paginatedFilter);
    }
}
