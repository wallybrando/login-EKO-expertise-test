using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;
using LoginEKO.FileProcessingService.Domain.Validators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoginEKO.FileProcessingService.Domain.Services
{
    public class FileService : IFileService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFileRepository _fileMetadataRepository;
        private readonly ILogger<FileService> _logger;

        public FileService(IServiceProvider serviceProvider, IFileRepository fileMetadataRepository, ILogger<FileService> logger)
        {
            _serviceProvider = serviceProvider;
            _fileMetadataRepository = fileMetadataRepository;
            _logger = logger;
        }

        public async Task<int> ImportVehicleTelemetryAsync(FileMetadata file, CancellationToken token = default)
        {
            var fileHashBytes = MD5Validator.ComputeHash(file.BinaryObject);
            var fileHash = MD5Validator.CreateHashStringFromHashBytes(fileHashBytes);

            var fileDb = await _fileMetadataRepository.GetByMD5HashAsync(fileHash, token);
            if (fileDb != null)
            {
                _logger.LogError("File has been corrupted");
                throw new FileValidationException("File has been corrupted");
            }

            var vehicleType = FileManager.GetVehicleTypeFromFilename(file.Filename);
            if (vehicleType == VehicleType.UNKNOWN)
            {
                _logger.LogError("Filename format is invalid");
                throw new FileValidationException("Name of file is invalid");
            }

            var fileType = FileManager.GetFileTypeFromExtension(file.Extension);
            if (fileType == FileType.UNKNOWN)
            {
                _logger.LogError("File type is not supported");
                throw new FileValidationException("File type is not supported");
            }

            var fileExtractor = _serviceProvider.GetServices<IFileExtractor>()
                                                .FirstOrDefault(x => x.Type == fileType)
                                                ??
                                                throw new ArgumentNullException("fileExtractor");

            var vehicleDataParser = _serviceProvider.GetServices<IVehicleDataParser>()
                                                           .FirstOrDefault(x => x.Type == vehicleType)
                                                           ??
                                                           throw new ArgumentNullException("vehicleDataParser");

            file.MD5Hash = fileHash;
            var extractedData = await fileExtractor.ExtractDataAsync(file.File, token);
            if (!extractedData.Any())
                return 0;

            var telemetries = vehicleDataParser.TransformVehicleData(extractedData);
            switch (vehicleType)
            {
                case VehicleType.TRACTOR: file.TractorTelemetries = (ICollection<TractorTelemetry>) telemetries;
                    break;
                case VehicleType.COMBINE: file.CombineTelemetries = (ICollection<CombineTelemetry>)telemetries;
                    break;
            }

            // minus 1 because one affected row is for filemetadata insert
            var dbRowsAffected = await _fileMetadataRepository.ImportFileAsync(file, token) - 1;

            if (dbRowsAffected <= 0)
                return 0;

            return dbRowsAffected;
        }
    }
}
