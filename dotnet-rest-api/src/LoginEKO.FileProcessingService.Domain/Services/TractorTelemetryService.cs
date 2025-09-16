using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;

namespace LoginEKO.FileProcessingService.Domain.Services
{
    public class TractorTelemetryService : ITractorTelemetryService
    {
        private readonly ITractorTelemetryRepository _tractorTelemetryRepository;

        public TractorTelemetryService(ITractorTelemetryRepository tractorTelemetryRepository)
        {
            _tractorTelemetryRepository = tractorTelemetryRepository;
        }

        public async Task<int> GetCountAsync(PaginatedFilter paginatedFilter, CancellationToken token = default)
        {
            var count = await _tractorTelemetryRepository.GetCountAsync(paginatedFilter, token);
            return count;
        }

        public async Task<IEnumerable<TractorTelemetry>> GetTractorTelemetriesAsync(PaginatedFilter paginatedFilter, CancellationToken token = default)
        {
            var result = await _tractorTelemetryRepository.GetAsync(paginatedFilter, token);
            return result;
        }
    }
}
