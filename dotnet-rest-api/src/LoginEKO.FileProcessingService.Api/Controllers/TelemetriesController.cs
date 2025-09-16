using LoginEKO.FileProcessingService.Api.Mapping;
using LoginEKO.FileProcessingService.Contracts.Requests;
using LoginEKO.FileProcessingService.Contracts.Responses;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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

            var tractorTelemetry = await _tractorTelemetryService.GetTractorTelemetriesAsync(filter, token);
            var totalTractorTelemetryCount = await _tractorTelemetryService.GetCountAsync(filter, token);

            var telemetry = new Dictionary<string, IEnumerable<object>>(1)
            {
                { nameof(TractorTelemetry), tractorTelemetry.MapToResponse() }
            };

            var response = CreatePagedTelemetryResponse(telemetry, filter.PageNumber, filter.PageSize, totalTractorTelemetryCount, 0);

            //var response = new PagedTelemetryResponse
            //{
            //    Page = pageNumber,
            //    PageSize = pageSize,
            //    TotalTractorItems = totalTractorTelemetryCount,
            //    TotalCombineItems = 0,
            //    TotalItems = totalTractorTelemetryCount + 0,
            //    Telemetry = telemetry
            //};

            return Ok(response);
        }

        [HttpPost(ApiEndpoints.Telemetries.Combines)]
        public async Task<IActionResult> GetAllCombineTelemetriesAsync([FromBody] IEnumerable<FilterRequest> request, [FromQuery] int? pageNumber, int? pageSize, CancellationToken token)
        {
            var filter = request.MapToPaginatedFilter(pageNumber, pageSize);

            var combineTelemetry = await _combineTelemetryService.GetCombineTelemetriesAsync(filter, token);
            var totalCombineTelemetryCount = await _combineTelemetryService.GetCountAsync(filter, token);

            var telemetry = new Dictionary<string, IEnumerable<object>>(1)
            {
                { nameof(CombineTelemetry), combineTelemetry.MapToResponse() }
            };

            var response = CreatePagedTelemetryResponse(telemetry, filter.PageNumber, filter.PageSize, 0, totalCombineTelemetryCount);

            //var response = new PagedTelemetryResponse
            //{
            //    Page = pageNumber,
            //    PageSize = pageSize,
            //    TotalTractorItems = 0,
            //    TotalCombineItems = totalCombineTelemetryCount,
            //    TotalItems = 0 + totalCombineTelemetryCount,
            //    Telemetry = telemetry
            //};

            return Ok(response);
        }

        [HttpPost(ApiEndpoints.Telemetries.All)]
        public async Task<IActionResult> GetAllAsync([FromBody] IEnumerable<FilterRequest> request, [FromQuery] int? pageNumber, int? pageSize, CancellationToken token)
        {
            var filter = request.MapToPaginatedFilter(pageNumber, pageSize);

            var tractorTelemetry = await _tractorTelemetryService.GetTractorTelemetriesAsync(filter, token);
            var totalTrractorTelemetryCount = await _tractorTelemetryService.GetCountAsync(filter, token);

            var combineTelemetry = await _combineTelemetryService.GetCombineTelemetriesAsync(filter, token);
            var totalCombineTelemetryCount = await _combineTelemetryService.GetCountAsync(filter, token);

            var tractorTelemetryResponse = tractorTelemetry.MapToResponse();
            var combineTelemetryResponse = combineTelemetry.MapToResponse();

            var telemetry = new Dictionary<string, IEnumerable<object>>()
            {
                { nameof(TractorTelemetry), tractorTelemetryResponse },
                { nameof(CombineTelemetry), combineTelemetryResponse }
            };

            var response = CreatePagedTelemetryResponse(telemetry, filter.PageNumber, filter.PageSize, totalTrractorTelemetryCount, totalCombineTelemetryCount);

            //var response = new PagedTelemetryResponse
            //{
            //    Page = pageNumber,
            //    PageSize = pageSize,
            //    TotalTractorItems = totalTrractorTelemetryCount,
            //    TotalCombineItems = totalCombineTelemetryCount,
            //    TotalItems = totalTrractorTelemetryCount + totalCombineTelemetryCount,
            //    Telemetry = telemetry
            //};

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
