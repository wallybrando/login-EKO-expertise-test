using LoginEKO.FileProcessingService.Api.Mapping;
using LoginEKO.FileProcessingService.Contracts.Requests.V1;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginEKO.FileProcessingService.Api.Controllers.V1
{
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FilesController> _logger;

        public FilesController(IFileService fileService, ILogger<FilesController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        [HttpPost(ApiEndpoints.V1.Files.Import)]
        public async Task<IActionResult> ImportVehicleTelemetryFileAsync([FromForm] ImportFileRequest request, CancellationToken token = default)
        {
            _logger.LogTrace("ImportVehicleTelemetryFileAsync(ImportFileRequest, CancellationToken)");
            var file = request.MapToFileMetadata();
            var numberOfImports = await _fileService.ImportVehicleTelemetryAsync(file, token);

            var response = file.MapToResponse();
            response.NumberOfImports = numberOfImports;

            _logger.LogDebug("Successfully returned response to client");
            return Accepted($"{ApiEndpoints.V1.Files.Get}/{response.Id}", response);
        }
    }
}
