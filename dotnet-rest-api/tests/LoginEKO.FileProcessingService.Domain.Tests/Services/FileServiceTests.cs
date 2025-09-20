using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Extensions;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace LoginEKO.FileProcessingService.Domain.Tests.Services
{
    [TestFixture]
    public class FileServiceTests
    {
        [Test]
        public void ImportVehicleTelemetryAsync_MD5HashColision_ThrowsFileValidationException()
        {
            // Arrange
            var filename = $"LD_A{DateTime.Now}.csv";
            var fileMetadata = CreateObject(filename: filename);

            var serviceProviderMock = new Mock<IServiceProvider>();
            var fileMetadataRepositoryMock = new Mock<IFileMetadataRepository>();
            var loggerMock = new Mock<ILogger<FileService>>();

            var service = new FileService(serviceProviderMock.Object, fileMetadataRepositoryMock.Object, loggerMock.Object);

            fileMetadataRepositoryMock
                .Setup(x => x.ExistsByMD5HashAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var ex = Assert.ThrowsAsync<FileValidationException>(async () => await service.ImportVehicleTelemetryAsync(fileMetadata));

            // Assert
            StringAssert.Contains("File has been corrupted", ex.Message);
        }

        [Test]
        public void ImportVehicleTelemetryAsync_InvalidFilename_ThrowsFileValidationException()
        {
            // Arrange
            var fileMetadata = CreateObject();

            var serviceProviderMock = new Mock<IServiceProvider>();
            var fileMetadataRepositoryMock = new Mock<IFileMetadataRepository>();
            var loggerMock = new Mock<ILogger<FileService>>();

            var service = new FileService(serviceProviderMock.Object, fileMetadataRepositoryMock.Object, loggerMock.Object);

            fileMetadataRepositoryMock
                .Setup(x => x.ExistsByMD5HashAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var ex = Assert.ThrowsAsync<FileValidationException>(async () => await service.ImportVehicleTelemetryAsync(fileMetadata));

            // Assert
            StringAssert.Contains("Name of file is invalid", ex.Message);
        }

        [Test]
        public void ImportVehicleTelemetryAsync_InvalidFileExtension_ThrowsFileValidationException()
        {
            // Arrange
            var filename = $"LD_A{DateTime.Now}.pdf";
            var extension = "application/pdf";
            var fileMetadata = CreateObject(filename: filename, extension: extension);

            var serviceProviderMock = new Mock<IServiceProvider>();
            var fileMetadataRepositoryMock = new Mock<IFileMetadataRepository>();
            var loggerMock = new Mock<ILogger<FileService>>();

            var service = new FileService(serviceProviderMock.Object, fileMetadataRepositoryMock.Object, loggerMock.Object);

            fileMetadataRepositoryMock
                .Setup(x => x.ExistsByMD5HashAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var ex = Assert.ThrowsAsync<FileValidationException>(async () => await service.ImportVehicleTelemetryAsync(fileMetadata));

            // Assert
            StringAssert.Contains("File type is not supported", ex.Message);
        }

        [Test]
        public void ImportVehicleTelemetryAsync_EmptyCollectionOfExtractedData_ThrowsFileValidationException()
        {
            // Arrange
            var filename = $"LD_A{DateTime.Now}.csv";
            var extension = "application/csv";
            var fileMetadata = CreateObject(filename: filename, extension: extension);

            var fileMetadataRepositoryMock = new Mock<IFileMetadataRepository>();
            var loggerMock = new Mock<ILogger<FileService>>();

            var fileExtractorMock = new Mock<IFileExtractor>();
            fileExtractorMock.Setup(x => x.ExtractDataAsync(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync([]);

            var services = new ServiceCollection();
            services.AddKeyedSingleton(FileType.CSV.GetDescription(), fileExtractorMock.Object);
            var serviceProvider = services.BuildServiceProvider();

            var service = new FileService(serviceProvider, fileMetadataRepositoryMock.Object, loggerMock.Object);

            fileMetadataRepositoryMock
                .Setup(x => x.ExistsByMD5HashAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var ex = Assert.ThrowsAsync<FileValidationException>(async () => await service.ImportVehicleTelemetryAsync(fileMetadata));

            // Assert
            StringAssert.Contains("Document cannot be empty", ex.Message);
        }

        [Test]
        public async Task ImportVehicleTelemetryAsync_ValidTractorTelemetryCollection_ReturnsOneAffectedRows()
        {
            // Arrange
            var filename = $"LD_A{DateTime.Now}.csv";
            var extension = "application/csv";
            var fileMetadata = CreateObject(filename: filename, extension: extension);

            var fileMetadataRepositoryMock = new Mock<IFileMetadataRepository>();
            var loggerMock = new Mock<ILogger<FileService>>();

            var fileExtractorMock = new Mock<IFileExtractor>();
            fileExtractorMock.Setup(x => x.ExtractDataAsync(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<string[]> { new string[] { "telemetry" } });

            var vehicleTelemetryParserMock = new Mock<IVehicleTelemetryParser>();
            vehicleTelemetryParserMock.Setup(x => x.ParseAgroVehicleTelemetry(It.IsAny<IEnumerable<string[]>>()))
                .Returns(new List<TractorTelemetry> { new TractorTelemetry() });

            fileMetadataRepositoryMock
                .Setup(x => x.ExistsByMD5HashAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            fileMetadataRepositoryMock
                .Setup(x => x.ImportFileMetadataAsync(It.IsAny<FileMetadata>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(2);

            var services = new ServiceCollection();
            services.AddKeyedSingleton(FileType.CSV.GetDescription(), fileExtractorMock.Object);
            services.AddKeyedSingleton(VehicleType.TRACTOR.GetDescription(), vehicleTelemetryParserMock.Object);
            var serviceProvider = services.BuildServiceProvider();

            var service = new FileService(serviceProvider, fileMetadataRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.ImportVehicleTelemetryAsync(fileMetadata);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task ImportVehicleTelemetryAsync_ValidCombineTelemetryCollection_ReturnsOneAffectedRows()
        {
            // Arrange
            var filename = $"LD_C{DateTime.Now}.csv";
            var extension = "application/csv";
            var fileMetadata = CreateObject(filename: filename, extension: extension);

            var fileMetadataRepositoryMock = new Mock<IFileMetadataRepository>();
            var loggerMock = new Mock<ILogger<FileService>>();

            var fileExtractorMock = new Mock<IFileExtractor>();
            fileExtractorMock.Setup(x => x.ExtractDataAsync(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<string[]> { new string[] { "telemetry" } });

            var vehicleTelemetryParserMock = new Mock<IVehicleTelemetryParser>();
            vehicleTelemetryParserMock.Setup(x => x.ParseAgroVehicleTelemetry(It.IsAny<IEnumerable<string[]>>()))
                .Returns(new List<CombineTelemetry> { new CombineTelemetry() });

            fileMetadataRepositoryMock
                .Setup(x => x.ExistsByMD5HashAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            fileMetadataRepositoryMock
                .Setup(x => x.ImportFileMetadataAsync(It.IsAny<FileMetadata>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(2);

            var services = new ServiceCollection();
            services.AddKeyedSingleton(FileType.CSV.GetDescription(), fileExtractorMock.Object);
            services.AddKeyedSingleton(VehicleType.COMBINE.GetDescription(), vehicleTelemetryParserMock.Object);
            var serviceProvider = services.BuildServiceProvider();

            var service = new FileService(serviceProvider, fileMetadataRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.ImportVehicleTelemetryAsync(fileMetadata);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        private FileMetadata CreateObject(Guid? Id = null, string? filename = null, string? extension = null, string? md5Hash = null)
        {
            return new FileMetadata
            {
                Id = Id ?? Guid.NewGuid(),
                MD5Hash = md5Hash ?? Guid.NewGuid().ToString(),
                SizeInBytes = 16,
                Filename = filename ?? $"file_{DateTime.Now}.csv",
                File = Mock.Of<IFormFile>(),
                CreatedDate = DateTime.Now,
                Extension = extension ?? "text/csv",
                BinaryObject = new byte[16]
            };
        }
    }
}
