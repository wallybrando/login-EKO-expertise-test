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
                Filename = request.File.FileName,
                Extension = request.File.ContentType.Split('/').Last(),
                SizeInBytes = request.File.Length,
                BinaryObject = ToByteArray(request.File),
                File = request.File,
            };
        }

        public static FileResponse MapToResponse(this FileDto file)
        {
            return new FileResponse
            {
                Id = file.Id,
                Filename = file.Filename,
                SizeInBytes = file.SizeInBytes,
                CreatedDate = file.CreatedDate,
            };
        }

        private static byte[] ToByteArray(IFormFile file)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }
    }
}
