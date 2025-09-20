using LoginEKO.FileProcessingService.Api.Controllers.V1;
using LoginEKO.FileProcessingService.Contracts.Requests.V1;
using LoginEKO.FileProcessingService.Contracts.Responses.V1.Files;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace LoginEKO.FileProcessingService.Api.Tests.Controllers.V1
{
    [TestFixture]
    public class FilesControllerTests
    {
        [Test]
        public async Task ImportVehicleTelemetryFileAsync_ValidCSVFile_ReturnsAccepted()
        {
            // Arrange
            var numberOfImports = 10;
            var filename = $"LD_A{DateTime.Now}.csv";
            var file = CreateFileObject(out var sizeInBytes, filename);
            var request = CreateImportFileRequestObject(file);

            var fileServiceMock = new Mock<IFileService>();
            var loggerMock = new Mock<ILogger<FilesController>>();

            var controller = new FilesController(fileServiceMock.Object, loggerMock.Object);

            fileServiceMock
                .Setup(x => x.ImportVehicleTelemetryAsync(It.IsAny<FileMetadata>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(numberOfImports);

            // Act
            var result = await controller.ImportVehicleTelemetryFileAsync(request);

            // Assert
            Assert.That(result, Is.InstanceOf<AcceptedResult>());
            var acceptedResponse = result as AcceptedResult;
            var response = (FileMetadataResponse)acceptedResponse!.Value!;
            Assert.That(filename, Is.EqualTo(response.Filename));
            Assert.That(numberOfImports, Is.EqualTo(response.NumberOfImports));
            Assert.That(sizeInBytes, Is.EqualTo(response.SizeInBytes));
        }

        private ImportFileRequest CreateImportFileRequestObject(IFormFile? file = null)
        {
            return new ImportFileRequest
            {
                File = file ?? new Mock<IFormFile>().Object
            };
        }

        private IFormFile CreateFileObject(out long sizeInBytes, string? filename = null, string? content = null, string? contentType = null)
        {
            filename = filename ?? "unit-test.csv";
            content = content ?? "Unit testing";
            contentType = contentType ?? "text/csv";

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(x => x.OpenReadStream()).Returns(stream);
            fileMock.Setup(x => x.FileName).Returns(filename);
            fileMock.Setup(x => x.ContentType).Returns(contentType);
            fileMock.Setup(x => x.Length).Returns(stream.Length);

            sizeInBytes = stream.Length;

            return fileMock.Object;
        }
    }
}
