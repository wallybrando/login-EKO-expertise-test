using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Services
{
    public interface ITractorTelemetryService
    {
        Task<IEnumerable<TractorTelemetry>> GetTractorTelemetriesAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
        //Task<IDictionary<Type, IEnumerable<Vehicle>>> GetAllTelemetriesAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
        Task<int> GetCountAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
    }
}
