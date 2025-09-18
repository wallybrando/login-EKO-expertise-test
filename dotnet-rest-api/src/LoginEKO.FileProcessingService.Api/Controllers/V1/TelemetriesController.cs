using LoginEKO.FileProcessingService.Api.Mapping;
using LoginEKO.FileProcessingService.Contracts.Requests.V1;
using LoginEKO.FileProcessingService.Contracts.Responses.V1;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LoginEKO.FileProcessingService.Api.Controllers.V1
{
    [ApiController]
    public class TelemetriesController : ControllerBase
    {
        private readonly ITelemetryService _telemetryService;
        public TelemetriesController(ITelemetryService telemetryService)
        {
            _telemetryService = telemetryService;
        }

        [HttpPost(ApiEndpoints.V1.Telemetries.All)]
        public async Task<IActionResult> GetAllAsync([FromBody] IEnumerable<FilterRequest> request, [FromQuery] int? pageNumber, int? pageSize, CancellationToken token)
        {
            var filter = request.MapToPaginatedFilter(pageNumber, pageSize);

             var unifedTelemetry = await _telemetryService.GetTractorTelemetriesAsync(filter, token);

            var telemetry = new Dictionary<string, IEnumerable<object>>()
            {
                { nameof(TractorTelemetry), unifedTelemetry.TractorTelemetry.MapToResponse() },
                { nameof(CombineTelemetry), unifedTelemetry.CombinesTelemetry.MapToResponse() }
            };

            var response = CreatePagedTelemetryResponse(telemetry, filter.PageNumber, filter.PageSize, unifedTelemetry.TotalTractorsTelemetryCount, unifedTelemetry.TotalCombinesTelemetryCount);

            return Ok(response);
        }

        private PagedTelemetryResponse CreatePagedTelemetryResponse(
            IDictionary<string, IEnumerable<object>> telemetry,
            int pageNumber,
            int pageSize,
            int totalTractorTelemetryCount,
            int totalCombineTelemetryCount)
        {
            return new PagedTelemetryResponse
            {
                Page = pageNumber,
                PageSize = pageSize,
                TotalTractorItems = totalTractorTelemetryCount,
                TotalCombineItems = totalCombineTelemetryCount,
                TotalItems = totalTractorTelemetryCount + totalCombineTelemetryCount,
                Telemetry = telemetry
            };
        }
    }
}
