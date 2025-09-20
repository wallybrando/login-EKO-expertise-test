using FluentAssertions.Execution;
using LoginEKO.FileProcessingService.Domain;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Persistence.Repositories;
using LoginEKO.FileProcessingService.Persistence.Tests.Database;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Persistence.Tests.Repositories
{
    [TestFixture]
    public class CombineTelemetryRepositoryTests
    {
        [Test]
        public async Task GetAsync_SerialNumberFilter_ReturnsTelemetry()
        {
            // Arrange
            var serialNumber = "Combine123";
            var serialNumberTuple = new List<(string, FilterOperation, object?, Type)>
            {
                ("SerialNumber", FilterOperation.EQUALS, serialNumber, typeof(string))
            };
            var expression = CreateExpressionForSerialNumber(serialNumberTuple);

            var dbContext = MockDbContext.CreateInMemoryDbContext(Guid.NewGuid().ToString());
            var loggerMock = new Mock<ILogger<CombineTelemetryRepository>>();

            var repository = new CombineTelemetryRepository(dbContext, loggerMock.Object);

            // Act
            var result = await repository.GetAsync(expression, 1, 10);

            // Assert
            using var scope = new AssertionScope();
            Assert.That(result, Is.InstanceOf<IEnumerable<CombineTelemetry>>());
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result.ElementAt(0).SerialNumber, Is.EqualTo(serialNumber));
                Assert.That(result.ElementAt(1).SerialNumber, Is.EqualTo(serialNumber));
            });
        }

        [Test]
        public async Task GetCountAsync_SerialNumberFilter_ReturnsTelemetry()
        {
            // Arrange
            var serialNumber = "Combine123";
            var serialNumberTuple = new List<(string, FilterOperation, object?, Type)>
            {
                ("SerialNumber", FilterOperation.EQUALS, serialNumber, typeof(string))
            };
            var expression = CreateExpressionForSerialNumber(serialNumberTuple);

            var dbContext = MockDbContext.CreateInMemoryDbContext(Guid.NewGuid().ToString());
            var loggerMock = new Mock<ILogger<CombineTelemetryRepository>>();

            var repository = new CombineTelemetryRepository(dbContext, loggerMock.Object);

            // Act
            var result = await repository.GetCountAsync(expression);

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }

        private Expression<Func<CombineTelemetry, bool>> CreateExpressionForSerialNumber(IEnumerable<(string, FilterOperation, object?, Type)> serialNumberTuple)
        {
            var predicate = new DynamicFilterBuilder<CombineTelemetry>();
            predicate.And(b => b.Or(serialNumberTuple!));
            return predicate.Build();
        }
    }
}
