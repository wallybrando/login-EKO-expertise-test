using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;

namespace LoginEKO.FileProcessingService.Domain.Services
{
    public class CombineTelemetryService : ICombineTelemetryService
    {
        private readonly ICombineTelemetryRepository _combineTelemetryRepository;

        public CombineTelemetryService(ICombineTelemetryRepository combineTelemetryRepository)
        {
            _combineTelemetryRepository = combineTelemetryRepository;
        }

        public async Task<IEnumerable<CombineTelemetry>> GetCombineTelemetriesAsync(PaginatedFilter paginatedFilter, CancellationToken token = default)
        {
            var result = await _combineTelemetryRepository.GetAsync(paginatedFilter, token);
            return result;
        }

        public async Task<int> GetCountAsync(PaginatedFilter paginatedFilter, CancellationToken token = default)
        {
            var result = await _combineTelemetryRepository.GetCountAsync(paginatedFilter, token);
            return result;
        }
    }
}
