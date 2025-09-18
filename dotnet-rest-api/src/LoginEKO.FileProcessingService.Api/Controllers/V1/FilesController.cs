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
        public async Task<IActionResult> ImportVehicleTelemetryFileAsync([FromForm] UploadFileRequest request, CancellationToken token = default)
        {
            var file = request.MapToFileMetadata();
            var numberOfImports = await _fileService.ImportVehicleTelemetryAsync(file, token);

            if (numberOfImports == 0)
            {
                _logger.LogWarning("No telemetry data imported from document {FileName}", request.File.FileName);
                return NoContent();
            }

            var response = file.MapToResponse();
            response.NumberOfImports = numberOfImports;
            return Accepted($"{ApiEndpoints.V1.Files.Get}/{response.Id}", response);
        }
    }
}
