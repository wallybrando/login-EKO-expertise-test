using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Services
{
    public interface ITelemetryService
    {
        Task<UnifiedTelemetry> GetTractorTelemetriesAsync(PaginatedFilter paginatedFilter, CancellationToken token = default);
    }
}
