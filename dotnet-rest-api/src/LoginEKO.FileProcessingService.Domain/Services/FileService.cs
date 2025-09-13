using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;
using LoginEKO.FileProcessingService.Domain.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace LoginEKO.FileProcessingService.Domain.Services
{
    public class FileService : IFileService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITractorTelemetryRepository _tractorTelemetryRepository;
        private readonly ICombineTelemetryRepository _combineTelemetryRepository;
        private readonly IFileRepository _fileMetadataRepository;

        public FileService(IServiceProvider serviceProvider,
            ITractorTelemetryRepository tractorTelemetryRepository,
            ICombineTelemetryRepository combineTelemetryRepository,
            IFileRepository fileMetadataRepository)
        {
            _serviceProvider = serviceProvider;
            _tractorTelemetryRepository = tractorTelemetryRepository;
            _combineTelemetryRepository = combineTelemetryRepository;
            _fileMetadataRepository = fileMetadataRepository;
        }

        public async Task<bool> ImportVehicleTelemetryAsync(FileMetadata file)
        {
            var fileHashBytes = MD5Validator.ComputeHash(file.BinaryObject);
            var fileHash = MD5Validator.CreateHashStringFromHashBytes(fileHashBytes);

            var fileDb = await _fileMetadataRepository.GetByMD5HashAsync(fileHash);
            if (fileDb != null)
            {
                throw new ArgumentException("File has been corupted");
            }

            file.MD5Hash = fileHash;

            var vehicleType = FileManager.GetVehicleTypeFromFilename(file.Filename);
            if (vehicleType == VehicleType.UNKNOWN)
                throw new ArgumentException("Unknown vehicle");

            var fileType = FileManager.GetFileTypeFromExtension(file.Extension);
            if (fileType == FileType.UNKNOWN)
                throw new ArgumentException("Unknown file type");

            var fileExtractor = _serviceProvider.GetServices<IFileExtractor>()
                                                .FirstOrDefault(x => x.Type == fileType)
                                                ??
                                                throw new ArgumentNullException();

            var vehicleDataTransformator = _serviceProvider.GetServices<IVehicleDataParser>()
                                                           .FirstOrDefault(x => x.Type == vehicleType)
                                                           ??
                                                           throw new ArgumentNullException();

            var extractedData = await fileExtractor.ExtractDataAsync(file.File);
            var telemetries = vehicleDataTransformator.TransformVehicleData(extractedData);

            if (extractedData.Count() != telemetries.Count())
            {

            }

            try
            {
                var fileMetadataCreated = await _fileMetadataRepository.CreateFileMetadataAsync(file);
                if (!fileMetadataCreated)
                    return false;

                var telemetryCreated = false;
                switch (vehicleType)
                {
                    case VehicleType.TRACTOR:
                        telemetryCreated = await _tractorTelemetryRepository.InsertTelemetryAsync((IEnumerable<TractorTelemetry>)telemetries);
                        break;
                    case VehicleType.COMBINE:
                        telemetryCreated = await _combineTelemetryRepository.InsertTelemetryAsync((IEnumerable<CombineTelemetry>)telemetries);
                        break;
                }

                if (!telemetryCreated)
                    throw new ArgumentException();

                return telemetryCreated;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
