using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<FileDto?> GetAsync(Guid id)
        {
            var file = await _fileRepository.GetAsync(id);

            if (file == null)
                return null;

            return file;
        }

        public Task<bool> UploadFileAsync(FileDto file)
        {
            // IsFileExtensionAllowed()
            // IsFileSizeWithinLimit()
            // FileWithSameNameExists()
            // FileWithSameMD5HashExists()

            file.CreatedDate = DateTime.UtcNow;
           _fileRepository.UploadFileAsync(file);
            return Task.FromResult(true);
        }
    }
}
