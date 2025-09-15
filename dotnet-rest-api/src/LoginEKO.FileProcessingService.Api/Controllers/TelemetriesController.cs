using LoginEKO.FileProcessingService.Api.Mapping;
using LoginEKO.FileProcessingService.Contracts.Requests;
using LoginEKO.FileProcessingService.Contracts.Responses;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginEKO.FileProcessingService.Api.Controllers
{
    [ApiController]
    public class TelemetriesController : ControllerBase
    {
        private readonly ITelemetryService _telemetryService;
        public TelemetriesController(ITelemetryService telemetryService)
        {
            _telemetryService = telemetryService;
        }


        [HttpPost(ApiEndpoints.Telemetries.Tractors)]
        public async Task<IActionResult> GetAllTractorTelemetriesAsync([FromBody] ICollection<FilterRequest> request, [FromQuery] int? pageNumber, int? pageSize)
        {
            var filter = request.MapToPaginatedFilter();
            filter.PageNumber = pageNumber;
            filter.PageSize = pageSize;

            var result = await _telemetryService.GetTractorTelemetriesAsync(filter);
            var response = result.MapToResponse();


            return Ok(response);
        }

        [HttpPost(ApiEndpoints.Telemetries.Combines)]
        public async Task<IActionResult> GetAllCombineTelemetriesAsync([FromBody] IEnumerable<FilterRequest> request, [FromQuery] int? pageNumber, int? pageSize)
        {
            var filter = request.MapToPaginatedFilter();
            filter.PageNumber = pageNumber;
            filter.PageSize = pageSize;

            var result = await _telemetryService.GetCombineTelemetriesAsync(filter);
            var response = result.MapToResponse();

            return Ok(response);
        }
    }
}
