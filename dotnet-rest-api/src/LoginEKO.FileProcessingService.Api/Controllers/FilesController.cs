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

        [HttpPost(ApiEndpoints.Vehicles.Import)]
        public async Task<IActionResult> ImportVehicleTelemetryFileAsync([FromForm] UploadFileRequest request)
        {
            var file = request.MapToFileDto();
            await _fileService.ImportVehicleTelemetryAsync(file);
            var response = file.MapToResponse();

            return Created($"{ApiEndpoints.Vehicles.Base}/{response.Id}", response);
        }
    }
}
