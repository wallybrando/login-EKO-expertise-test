using LoginEKO.FileProcessingService.Api.Mapping;
using LoginEKO.FileProcessingService.Contracts.Requests;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginEKO.FileProcessingService.Api.Controllers
{
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost(ApiEndpoints.Files.Import)]
        public async Task<IActionResult> ImportVehicleTelemetryFileAsync([FromForm] UploadFileRequest request)
        {
            var file = request.MapToFileMetadata();
            var numberOfImports = await _fileService.ImportVehicleTelemetryAsync(file);

            if (numberOfImports == 0)
            {
                return NoContent();
            }

            var response = file.MapToResponse();
            response.NumberOfImports = numberOfImports;
            return Accepted($"{ApiEndpoints.Files.Base}/{response.Id}", response);
        }
    }
}
