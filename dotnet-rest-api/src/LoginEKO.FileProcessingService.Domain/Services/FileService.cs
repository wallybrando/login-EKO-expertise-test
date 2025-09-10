using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace LoginEKO.FileProcessingService.Domain.Services
{
    public class FileService : IFileService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFileRepository _fileRepository;

        public FileService(IServiceProvider serviceProvider, ITextFileExtractor fileExtractor, IFileRepository fileRepository)
        {
            _serviceProvider = serviceProvider;
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

            var vehicleType = GetVehicleType(file.Filename);
            if (vehicleType == VehicleType.UNKNOWN)
                throw new ArgumentException();

            var fileType = GetFileType(file.Extension);
            if (fileType == FileType.UNKNOWN)
                throw new ArgumentException();

            var fileExtractor = _serviceProvider.GetServices<ITextFileExtractor>()
                                                .FirstOrDefault(x => x.Type == fileType)
                                                ??
                                                throw new ArgumentNullException();

            var vehicleDataTransformator = _serviceProvider.GetServices<IVehicleDataTransformator>()
                                                           .FirstOrDefault(x => x.Type == vehicleType)
                                                           ??
                                                           throw new ArgumentNullException();

            var extractedData = await fileExtractor.ExtractDataAsync(file.File);
            var transformedData = vehicleDataTransformator.TransformVehicleData(extractedData);

            switch (vehicleType)
            {
                case VehicleType.TRACTOR:
                    // call repository
                    break;
                case VehicleType.COMBINE:
                    // call repository
                    break;
                case VehicleType.UNKNOWN:
                default:
                    throw new ArgumentException();
            }

            throw new NotImplementedException();
        }

        private VehicleType GetVehicleType(string filename)
        {
            if (filename.StartsWith("LD_A", StringComparison.Ordinal))
                return VehicleType.TRACTOR;
            if (filename.StartsWith("LD_C", StringComparison.Ordinal))
                return VehicleType.COMBINE;

            return VehicleType.UNKNOWN;
        }

        private FileType GetFileType(string fileType)
        {
            fileType = fileType.Split('/').Last();
            if (fileType.Equals("CSV", StringComparison.OrdinalIgnoreCase))
                return FileType.CSV;

            return FileType.UNKNOWN;
        }
    }
}
