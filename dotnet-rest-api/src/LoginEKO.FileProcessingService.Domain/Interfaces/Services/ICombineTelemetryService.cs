using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Services
{
    public interface ICombineTelemetryService
    {
        Task<IEnumerable<CombineTelemetry>> GetCombineTelemetriesAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
        Task<int> GetCountAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
    }
}
