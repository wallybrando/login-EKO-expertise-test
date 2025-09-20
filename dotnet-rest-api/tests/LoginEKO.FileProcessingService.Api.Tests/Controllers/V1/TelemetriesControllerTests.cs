using FluentAssertions.Execution;
using LoginEKO.FileProcessingService.Api.Controllers.V1;
using LoginEKO.FileProcessingService.Contracts.Requests.V1;
using LoginEKO.FileProcessingService.Contracts.Responses.V1.Telemetries;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace LoginEKO.FileProcessingService.Api.Tests.Controllers.V1
{
    [TestFixture]
    public class TelemetriesControllerTests
    {
        [Test]
        public async Task GetAllAsync_ValidFilterRequest_ReturnsOkResponse()
        {
            // Arrange
            var serialNumberFilter = CreateFilterRequestObject(value: "ABC1234");
            var dateFilter = CreateFilterRequestObject("Date", "GreaterThan", "01-01-2025 10:00:00 AM");
            var filters = new FilterRequest[2] { serialNumberFilter, dateFilter };
            int? pageNumber = 1;
            int? pageSize = 10;


            var totalTractorTelemetryCount = 1254;
            var totalCombineTelemetryCount = 1388;
            var unifiedResult = new UnifiedTelemetry
            {
                TotalTractorsTelemetryCount = totalTractorTelemetryCount,
                TractorTelemetry = new List<TractorTelemetry>(pageSize!.Value),
                TotalCombinesTelemetryCount = totalCombineTelemetryCount,
                CombinesTelemetry = new List<CombineTelemetry>(pageSize!.Value)
            };

            var telemetryService = new Mock<ITelemetryService>();
            var loggerMock = new Mock<ILogger<TelemetriesController>>();

            var controller = new TelemetriesController(telemetryService.Object, loggerMock.Object);

            telemetryService
                .Setup(x => x.GetUnifiedTelemetriesAsync(It.IsAny<PaginatedFilter>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(unifiedResult);

            // Act
            var result = await controller.GetAllAsync(filters, pageNumber, pageSize);

            // Assert
            using var scope = new AssertionScope();
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResponse = result as OkObjectResult;
            var response = (PagedTelemetryResponse)okResponse!.Value!;

            Assert.Multiple(() =>
            {
                Assert.That(response.Page, Is.EqualTo(pageNumber));
                Assert.That(response.PageSize, Is.EqualTo(pageSize));
                Assert.That(response.TotalTractorItems, Is.EqualTo(totalTractorTelemetryCount));
                Assert.That(response.TotalCombineItems, Is.EqualTo(totalCombineTelemetryCount));
                Assert.That(response.TotalItems, Is.EqualTo(totalTractorTelemetryCount + totalCombineTelemetryCount));
                Assert.That(response.Telemetry.Count, Is.EqualTo(2));
            });
        }

        private FilterRequest CreateFilterRequestObject(string? field = null, string? operation = null, object? value = null)
        {
            return new FilterRequest
            {
                Field = field ?? "SerialNumber",
                Operation = operation ?? "Equals",
                Value = value
            };
        }
    }
}
