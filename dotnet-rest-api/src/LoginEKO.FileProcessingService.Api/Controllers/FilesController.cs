using LoginEKO.FileProcessingService.Api.Mapping;
using LoginEKO.FileProcessingService.Contracts.Requests;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginEKO.FileProcessingService.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("files")]
        public async Task<IActionResult> UploadAsync([FromBody] UploadFileRequest request)
        {
            var file = request.MapToFileDto();
            await _fileService.UploadFileAsync(file);
            var response = file.MapToResponse();
            return Created($"/api/files/{response.Id}", response);
        }
    }
}
