using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace LoginEKO.FileProcessingService.Domain.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITractorTelemetryRepository _tractorTelemetryRepository;
        private readonly ICombineTelemetryRepository _combineTelemetryRepository;
        private readonly IFileMetadataRepository _fileMetadataRepository;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public VehicleService(IServiceProvider serviceProvider,
            ITractorTelemetryRepository tractorTelemetryRepository,
            ICombineTelemetryRepository combineTelemetryRepository,
            IFileMetadataRepository fileMetadataRepository,
            IDbConnectionFactory dbConnectionFactory)
        {
            _serviceProvider = serviceProvider;
            _tractorTelemetryRepository = tractorTelemetryRepository;
            _combineTelemetryRepository = combineTelemetryRepository;
            _fileMetadataRepository = fileMetadataRepository;
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<bool> ImportTelemetryAsync(FileMetadata file)
        {
            var fileHashBytes = MD5Validator.ComputeHash(file.BinaryObject);
            var fileHash = MD5Validator.CreateHashStringFromHashBytes(fileHashBytes);

            using var connection = await _dbConnectionFactory.CreateConnectionAsync();

            var fileDb = await _fileMetadataRepository.GetByMD5HashAsync(fileHash, connection);
            if (fileDb != null)
            {
                throw new ArgumentException("File has been corupted");
            }

            file.MD5Hash = fileHash;

            var vehicleType = GetVehicleType(file.Filename);
            if (vehicleType == VehicleType.UNKNOWN) throw new ArgumentException();

            var fileType = GetFileType(file.Extension);
            if (fileType == FileType.UNKNOWN) throw new ArgumentException();

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

            using var transaction = connection.BeginTransaction();
            try
            {
                var fileMetadataCreated = await _fileMetadataRepository.CreateFileMetadataAsync(file, connection, transaction);
                if (!fileMetadataCreated)
                    return false;

                var telemetryCreated = false;
                switch (vehicleType)
                {
                    case VehicleType.TRACTOR:
                        telemetryCreated = await _tractorTelemetryRepository.InsertTelemetryAsync((IEnumerable<TractorTelemetry>)transformedData, connection, transaction);
                        break;
                    case VehicleType.COMBINE:
                        telemetryCreated = await _combineTelemetryRepository.InsertTelemetryAsync((Combine)transformedData, connection, transaction);
                        break;
                    case VehicleType.UNKNOWN:
                    default:
                        throw new ArgumentException();
                }

                if (!telemetryCreated)
                    throw new ArgumentException();

                transaction.Commit();

                return telemetryCreated;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
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
