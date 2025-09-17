using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Entities;

namespace LoginEKO.FileProcessingService.Domain.Services
{
    public class CombineTelemetryService : ICombineTelemetryService
    {
        private readonly ICombineTelemetryRepository _combineTelemetryRepository;

        public CombineTelemetryService(ICombineTelemetryRepository combineTelemetryRepository)
        {
            _combineTelemetryRepository = combineTelemetryRepository;
        }

        /// <summary>Returns paginated telemetry data based on applied filter</summary>
        public async Task<IEnumerable<CombineTelemetry>> GetCombineTelemetriesAsync(PaginatedFilter paginatedFilter, CancellationToken token = default)
        {
            var result = await _combineTelemetryRepository.GetAsync(paginatedFilter, token);
            return result;
        }

        /// <summary>Returns total number of affected telemetry data based on applied filter</summary>
        public async Task<int> GetCountAsync(PaginatedFilter paginatedFilter, CancellationToken token = default)
        {
            var result = await _combineTelemetryRepository.GetCountAsync(paginatedFilter, token);
            return result;
        }
    }
}
