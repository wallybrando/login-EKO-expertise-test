using LoginEKO.FileProcessingService.Api.Mapping;
using LoginEKO.FileProcessingService.Contracts.Requests.V1;
using LoginEKO.FileProcessingService.Contracts.Responses.V1.Telemetries;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LoginEKO.FileProcessingService.Api.Controllers.V1
{
    [ApiController]
    public class TelemetriesController : ControllerBase
    {
        private readonly ITelemetryService _telemetryService;
        private readonly ILogger<TelemetriesController> _logger;
        public TelemetriesController(ITelemetryService telemetryService, ILogger<TelemetriesController> logger)
        {
            _telemetryService = telemetryService;
            _logger = logger;
        }

        [HttpPost(ApiEndpoints.V1.Telemetries.All)]
        public async Task<IActionResult> GetAllAsync([FromBody] IEnumerable<FilterRequest> request, [FromQuery] int pageNumber, int pageSize, CancellationToken token = default)
        {
            _logger.LogTrace("GetAllAsync(FilterRequest[], pageNumber, pageSize, CancellationToken)");
            var filter = request.MapToPaginatedFilter(pageNumber, pageSize);

            var unifedTelemetry = await _telemetryService.GetUnifiedTelemetriesAsync(filter, token);

            var response = CreateResponse(unifedTelemetry, filter.PageNumber, filter.PageSize);

            _logger.LogDebug("Successfully returned response to client");
            return Ok(response);
        }

        private PagedTelemetryResponse CreateResponse(UnifiedTelemetry telemetry, int pageNumber, int pageSize)
        {
            return new PagedTelemetryResponse
            {
                Page = pageNumber,
                PageSize = pageSize,
                TotalTractorItems = telemetry.TotalTractorsTelemetryCount,
                TotalCombineItems = telemetry.TotalCombinesTelemetryCount,
                TotalItems = telemetry.TotalTractorsTelemetryCount + telemetry.TotalCombinesTelemetryCount,
                Telemetry = new Dictionary<string, IEnumerable<object>>
                {
                    { nameof(TractorTelemetry), telemetry.TractorTelemetry.MapToResponse() },
                    { nameof(CombineTelemetry), telemetry.CombinesTelemetry.MapToResponse() }
                }
            };
        }
    }
}
