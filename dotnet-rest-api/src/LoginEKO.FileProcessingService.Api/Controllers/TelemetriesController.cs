using LoginEKO.FileProcessingService.Api.Mapping;
using LoginEKO.FileProcessingService.Contracts.Requests;
using LoginEKO.FileProcessingService.Contracts.Responses;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginEKO.FileProcessingService.Api.Controllers
{
    [ApiController]
    public class TelemetriesController : ControllerBase
    {
        private readonly ITractorTelemetryService _tractorTelemetryService;
        private readonly ICombineTelemetryService _combineTelemetryService;
        public TelemetriesController(ITractorTelemetryService tractorTelemetryService, ICombineTelemetryService combineTelemetryService)
        {
            _tractorTelemetryService = tractorTelemetryService;
            _combineTelemetryService = combineTelemetryService;
        }

        [HttpPost(ApiEndpoints.Telemetries.Tractors)]
        public async Task<IActionResult> GetAllTractorTelemetriesAsync([FromBody] IEnumerable<FilterRequest> request, [FromQuery] int? pageNumber, int? pageSize, CancellationToken token)
        {
            var filter = request.MapToPaginatedFilter(pageNumber, pageSize);

            var result = await _tractorTelemetryService.GetTractorTelemetriesAsync(filter, token);
            var response = result.MapToResponse();

            return Ok(response);
        }

        [HttpPost(ApiEndpoints.Telemetries.Combines)]
        public async Task<IActionResult> GetAllCombineTelemetriesAsync([FromBody] IEnumerable<FilterRequest> request, [FromQuery] int? pageNumber, int? pageSize, CancellationToken token)
        {
            var filter = request.MapToPaginatedFilter(pageNumber, pageSize);

            var result = await _combineTelemetryService.GetCombineTelemetriesAsync(filter, token);
            var response = result.MapToResponse();

            return Ok(response);
        }

        [HttpPost(ApiEndpoints.Telemetries.All)]
        public async Task<IActionResult> GetAllAsync([FromBody] IEnumerable<FilterRequest> request, [FromQuery] int? pageNumber, int? pageSize, CancellationToken token)
        {
            var filter = request.MapToPaginatedFilter(pageNumber, pageSize);

            var tractorTelemetry = await _tractorTelemetryService.GetTractorTelemetriesAsync(filter, token);
            var totalTractorItems = await _tractorTelemetryService.GetCountAsync(filter, token);

            var combineTelemetry = await _combineTelemetryService.GetCombineTelemetriesAsync(filter, token);
            var totalCombineItems = await _combineTelemetryService.GetCountAsync(filter, token);

            var tractorTelemetryResponse = tractorTelemetry.MapToResponse();
            var combineTelemetryResponse = combineTelemetry.MapToResponse();

            var telemetryResponse = new Dictionary<string, IEnumerable<object>>()
            {
                { nameof(TractorTelemetry), tractorTelemetryResponse },
                { nameof(CombineTelemetry), combineTelemetryResponse }
            };

            var response = new PagedTelemetryResponse
            {
                Page = pageNumber,
                PageSize = pageSize,
                TotalTractorItems = totalTractorItems,
                TotalCombineItems = totalCombineItems,
                TotalItems = totalTractorItems + totalCombineItems,
                Telemetry = telemetryResponse
            };

            return Ok(response);
        }
    }
}
