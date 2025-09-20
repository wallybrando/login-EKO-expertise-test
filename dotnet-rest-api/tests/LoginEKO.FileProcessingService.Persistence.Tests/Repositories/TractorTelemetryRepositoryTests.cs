using FluentAssertions.Execution;
using LoginEKO.FileProcessingService.Domain;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Persistence.Repositories;
using LoginEKO.FileProcessingService.Persistence.Tests.Database;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace LoginEKO.FileProcessingService.Persistence.Tests.Repositories
{
    [TestFixture]
    public class TractorTelemetryRepositoryTests
    {
        [Test]
        public async Task GetAsync_SerialNumberFilter_ReturnsTelemetry()
        {
            // Arrange
            var serialNumber = "Tractor123";
            var serialNumberTuple = new List<(string, FilterOperation, object?, Type)>
            {
                ("SerialNumber", FilterOperation.EQUALS, serialNumber, typeof(string))
            };
            var expression = CreateExpressionForSerialNumber(serialNumberTuple);

            var dbContext = MockDbContext.CreateInMemoryDbContext(Guid.NewGuid().ToString());
            var loggerMock = new Mock<ILogger<TractorTelemetryRepository>>();

            var repository = new TractorTelemetryRepository(dbContext, loggerMock.Object);

            // Act
            var result = await repository.GetAsync(expression, 1, 10);

            // Assert
            using var scope = new AssertionScope();
            Assert.That(result, Is.InstanceOf<IEnumerable<TractorTelemetry>>());
            Assert.That(result.Count, Is.EqualTo(4));
            Assert.Multiple(() =>
            {
                Assert.That(result.ElementAt(0).SerialNumber, Is.EqualTo(serialNumber));
                Assert.That(result.ElementAt(1).SerialNumber, Is.EqualTo(serialNumber));
                Assert.That(result.ElementAt(2).SerialNumber, Is.EqualTo(serialNumber));
                Assert.That(result.ElementAt(3).SerialNumber, Is.EqualTo(serialNumber));
            });
        }

        [Test]
        public async Task GetCountAsync_SerialNumberFilter_ReturnsTelemetry()
        {
            // Arrange
            var serialNumber = "Tractor123";
            var serialNumberTuple = new List<(string, FilterOperation, object?, Type)>
            {
                ("SerialNumber", FilterOperation.EQUALS, serialNumber, typeof(string))
            };
            var expression = CreateExpressionForSerialNumber(serialNumberTuple);

            var dbContext = MockDbContext.CreateInMemoryDbContext(Guid.NewGuid().ToString());
            var loggerMock = new Mock<ILogger<TractorTelemetryRepository>>();

            var repository = new TractorTelemetryRepository(dbContext, loggerMock.Object);

            // Act
            var result = await repository.GetCountAsync(expression);

            // Assert
            Assert.That(result, Is.EqualTo(4));
        }

        private Expression<Func<TractorTelemetry, bool>> CreateExpressionForSerialNumber(IEnumerable<(string, FilterOperation, object?, Type)> serialNumberTuple)
        {
            var predicate = new DynamicFilterBuilder<TractorTelemetry>();
            predicate.And(b => b.Or(serialNumberTuple!));
            return predicate.Build();
        }
    }
}
