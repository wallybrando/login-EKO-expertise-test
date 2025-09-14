using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;

namespace LoginEKO.FileProcessingService.Domain.Services
{
    public class TelemetryService : ITelemetryService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITractorTelemetryRepository _tractorTelemetryRepository;
        private readonly ICombineTelemetryRepository _combineTelemetryRepository;

        public TelemetryService(IServiceProvider serviceProvider, ITractorTelemetryRepository tractorTelemetryRepository, ICombineTelemetryRepository combineTelemetryRepository)
        {
            _serviceProvider = serviceProvider;
            _tractorTelemetryRepository = tractorTelemetryRepository;
            _combineTelemetryRepository = combineTelemetryRepository;
        }

        public async Task<IEnumerable<CombineTelemetry>> GetCombineTelemetriesAsync(PaginatedFilter paginatedFilter)
        {
            var result = await _combineTelemetryRepository.GetAsync(paginatedFilter);
            return result;
        }

        public async Task<IEnumerable<TractorTelemetry>> GetTractorTelemetriesAsync(PaginatedFilter paginatedFilter)
        {
            var result = await _tractorTelemetryRepository.GetAsync(paginatedFilter);
            return result;
        }
    }
}
