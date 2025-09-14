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
        private readonly IFileRepository _fileMetadataRepository;

        public FileService(IServiceProvider serviceProvider, IFileRepository fileMetadataRepository)
        {
            _serviceProvider = serviceProvider;
            _fileMetadataRepository = fileMetadataRepository;
        }

        public async Task<int> ImportVehicleTelemetryAsync(FileMetadata file)
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
            if (extractedData.Count() == 0)
                return 0;

            var telemetries = vehicleDataTransformator.TransformVehicleData(extractedData);
            switch (vehicleType)
            {
                case VehicleType.TRACTOR: file.TractorTelemetries = (ICollection<TractorTelemetry>) telemetries;
                    break;
                case VehicleType.COMBINE: file.CombineTelemetries = (ICollection<CombineTelemetry>)telemetries;
                    break;
            }

            var telemetryRecordsImported = await _fileMetadataRepository.ImportFileAsync(file);

            if (telemetryRecordsImported == 0)
                return 0;

            // minus 1 because one affected row is for filemetadata insert
            return telemetryRecordsImported - 1;
        }
    }
}
