using LoginEKO.FileProcessingService.Contracts.Requests;
using LoginEKO.FileProcessingService.Contracts.Responses;
using LoginEKO.FileProcessingService.Domain.Models;
using System.Runtime.CompilerServices;

namespace LoginEKO.FileProcessingService.Api.Mapping
{
    public static class ContractMapping
    {
        public static FileDto MapToFileDto(this UploadFileRequest request)
        {
            return new FileDto
            {
                Id = Guid.NewGuid(),
                Filename = request.Filename,
                Size = request.Size
            };
        }

        public static FileResponse MapToResponse(this FileDto file)
        {
            return new FileResponse
            {
                Id = file.Id,
                Filename = file.Filename,
                Size = file.Size,
                CreatedAt = file.CreatedAt,
            };
        }
    }
}
