using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Validators;
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
            var file = await _fileRepository.GetByIdAsync(id);

            if (file == null)
                return null;

            return file;
        }

        public async Task<bool> UploadFileAsync(FileDto file)
        {

            // IsFileExtensionAllowed()
            // IsFileSizeWithinLimit()
            // FileWithSameNameExists()
            // FileWithSameMD5HashExists()

            var fileHashBytes = MD5Validator.ComputeHash(file.BinaryObject);

            var fileDb = await _fileRepository.GetByFilenameAsync(file.Filename);
            if (fileDb != null)
            {
                if (!MD5Validator.Validate(fileHashBytes, fileDb.MD5Hash))
                {
                    // file is corupted - same name but content is different
                    throw new ArgumentException("File is corupted");
                }

                throw new ArgumentException("File already exists");
            }

            file.MD5Hash = MD5Validator.CreateHashStringFromHashBytes(fileHashBytes);
            file.CreatedDate = DateTime.UtcNow;

            return await _fileRepository.UploadFileAsync(file);
        }
    }
}
