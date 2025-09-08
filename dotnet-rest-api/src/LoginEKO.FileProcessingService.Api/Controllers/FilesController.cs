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

        [HttpPost(ApiEndpoints.Files.Upload)]
        public async Task<IActionResult> UploadAsync([FromForm] UploadFileRequest request)
        {
            var file = request.MapToFileDto();
            await _fileService.UploadFileAsync(file);
            var response = file.MapToResponse();

            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpGet(ApiEndpoints.Files.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var file = await _fileService.GetAsync(id);

            if (file == null)
                return NotFound();

            var response = file.MapToResponse();
            return Ok(response);
        }
    }
}
