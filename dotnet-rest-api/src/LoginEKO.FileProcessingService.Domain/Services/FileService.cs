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
                throw new ArgumentException("File has been corrupted");
            }

            file.MD5Hash = fileHash;

            var vehicleType = FileManager.GetVehicleTypeFromFilename(file.Filename);
            if (vehicleType == VehicleType.UNKNOWN)
            {
                _logger.LogError("Filename format is invalid");
                throw new ArgumentException("Invalid filename format");
            }

            var fileType = FileManager.GetFileTypeFromExtension(file.Extension);
            if (fileType == FileType.UNKNOWN)
            {
                _logger.LogError("File type is not supported");
                throw new ArgumentException("Unknown file type");
            }

            var fileExtractor = _serviceProvider.GetServices<IFileExtractor>()
                                                .FirstOrDefault(x => x.Type == fileType)
                                                ??
                                                throw new ArgumentNullException("fileExtractor");

            var vehicleDataParser = _serviceProvider.GetServices<IVehicleDataParser>()
                                                           .FirstOrDefault(x => x.Type == vehicleType)
                                                           ??
                                                           throw new ArgumentNullException("vehicleDataParser");

            var extractedData = await fileExtractor.ExtractDataAsync(file.File, token);
            if (extractedData.Count() == 0)
                return 0;

            var telemetries = vehicleDataParser.TransformVehicleData(extractedData);
            switch (vehicleType)
            {
                case VehicleType.TRACTOR: file.TractorTelemetries = (ICollection<TractorTelemetry>) telemetries;
                    break;
                case VehicleType.COMBINE: file.CombineTelemetries = (ICollection<CombineTelemetry>)telemetries;
                    break;
            }

            var telemetryRecordsImported = await _fileMetadataRepository.ImportFileAsync(file, token);

            if (telemetryRecordsImported == 0)
                return 0;

            // minus 1 because one affected row is for filemetadata insert
            return telemetryRecordsImported - 1;
        }
    }
}
