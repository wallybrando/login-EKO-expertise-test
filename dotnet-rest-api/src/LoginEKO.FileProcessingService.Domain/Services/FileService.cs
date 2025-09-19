using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Extensions;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
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
        private readonly IFileMetadataRepository _fileMetadataRepository;
        private readonly ILogger<FileService> _logger;

        public FileService(IServiceProvider serviceProvider, IFileMetadataRepository fileMetadataRepository, ILogger<FileService> logger)
        {
             _serviceProvider = serviceProvider;
            _fileMetadataRepository = fileMetadataRepository;
            _logger = logger;
        }

        /// <summary> Import telemetry for agro vehicle with ETL (extract-transform-load) approach </summary>
        public async Task<int> ImportVehicleTelemetryAsync(FileMetadata file, CancellationToken token = default)
        {
            /*** Validate input parameters ****************************/
            var vehicleType = FileHandler.GetVehicleTypeFromFilename(file.Filename);
            if (vehicleType == VehicleType.UNKNOWN)
            {
                _logger.LogError("Filename format is invalid");
                throw new FileValidationException("Name of file is invalid");
            }

            var fileType = FileHandler.GetFileTypeFromExtension(file.Extension);
            if (fileType == FileType.UNKNOWN)
            {
                _logger.LogError("File type is not supported");
                throw new FileValidationException("File type is not supported");
            }

            /*** Validate file's MD5 Hash *****************************/
            var fileHashBytes = MD5Validator.ComputeHash(file.BinaryObject);
            var fileHash = MD5Validator.CreateHashStringFromHashBytes(fileHashBytes);

            var fileExists = await _fileMetadataRepository.ExistsByMD5HashAsync(fileHash, token);
            if (fileExists)
            {
                _logger.LogError("File has been corrupted");
                throw new FileValidationException("File has been corrupted");
            }

            var fileExtractor = _serviceProvider.GetRequiredKeyedService<IFileExtractor>(fileType.GetDescription());

            /*** Extract telemetry data from file **************************************/
            var extractedData = await fileExtractor.ExtractDataAsync(file.File, token);
            if (!extractedData.Any())
            {
                throw new FileValidationException("Document cannot be empty");
            }

            /*** Transform telemetry data **********************************************/
            var telemetryParser = _serviceProvider.GetRequiredKeyedService<IVehicleTelemetryParser>(vehicleType.GetDescription());
            var telemetries = telemetryParser.ParseAgroVehicleTelemetry(extractedData);
            switch (vehicleType)
            {
                case VehicleType.TRACTOR: file.TractorTelemetries = (List<TractorTelemetry>)telemetries;
                    break;
                case VehicleType.COMBINE: file.CombineTelemetries = (List<CombineTelemetry>)telemetries;
                    break;
            }

            /*** Load telemety in Database *********************************************/
            file.MD5Hash = fileHash;
            var dbRowsAffected = await _fileMetadataRepository.ImportFileMetadataAsync(file, token) - 1; // minus 1 because one affected row is for filemetadata insert

            return dbRowsAffected;
        }
    }
}
