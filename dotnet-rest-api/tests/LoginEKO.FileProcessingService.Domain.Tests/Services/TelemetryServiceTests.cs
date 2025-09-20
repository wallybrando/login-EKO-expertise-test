using FluentAssertions.Execution;
using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Services;
using LoginEKO.FileProcessingService.Domain.Validators;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace LoginEKO.FileProcessingService.Domain.Tests.Services
{
    [TestFixture]
    public class TelemetryServiceTests
    {
        [Test]
        public void GetUnifiedTelemetriesAsync_DuplicatedFilters_ThrowsFilterValidationException()
        {
            // Arrange
            var dateFilter1 = CreateFilterObject("Date", FilterOperation.EQUALS);
            var dateFilter2 = CreateFilterObject("Date", FilterOperation.EQUALS);
            var paginatedFilter = CreatePaginatedFilterObject(filters: [dateFilter1, dateFilter2]);

            var tractorTelemetryRepositoryMock = new Mock<ITractorTelemetryRepository>();
            var combineTelemetryRepositoryMock = new Mock<ICombineTelemetryRepository>();
            var tractorSchemaRegistryMock = new Mock<SchemaRegistry<TractorTelemetry>>();
            var combineSchemaRegistryMock = new Mock<SchemaRegistry<CombineTelemetry>>();
            var tractorFilterValidator = new FilterValidator<TractorTelemetry>(tractorSchemaRegistryMock.Object);
            var combineFilterValidator = new FilterValidator<CombineTelemetry>(combineSchemaRegistryMock.Object);
            var loggerMock = new Mock<ILogger<TelemetryService>>();

            var service = new TelemetryService(tractorTelemetryRepositoryMock.Object,
                                               combineTelemetryRepositoryMock.Object,
                                               tractorFilterValidator,
                                               combineFilterValidator,
                                               tractorSchemaRegistryMock.Object,
                                               combineSchemaRegistryMock.Object,
                                               loggerMock.Object);

            // Act
            var ex = Assert.ThrowsAsync<FilterValidationException>(async () => await service.GetUnifiedTelemetriesAsync(paginatedFilter));

            // Assert
            StringAssert.Contains("Duplicated filters' parameters in filter criteria", ex.Message);
        }

        [Test]
        public void GetUnifiedTelemetriesAsync_InvalidFilterField_ThrowsFilterValidationException()
        {
            // Arrange
            var invalidFieldName = "Date123";
            var dateFilter = CreateFilterObject(invalidFieldName, FilterOperation.EQUALS);
            var paginatedFilter = CreatePaginatedFilterObject(filters: [dateFilter]);

            var tractorTelemetryRepositoryMock = new Mock<ITractorTelemetryRepository>();
            var combineTelemetryRepositoryMock = new Mock<ICombineTelemetryRepository>();
            var tractorSchemaRegistryMock = new Mock<SchemaRegistry<TractorTelemetry>>();
            var combineSchemaRegistryMock = new Mock<SchemaRegistry<CombineTelemetry>>();
            var tractorFilterValidator = new FilterValidator<TractorTelemetry>(tractorSchemaRegistryMock.Object);
            var combineFilterValidator = new FilterValidator<CombineTelemetry>(combineSchemaRegistryMock.Object);
            var loggerMock = new Mock<ILogger<TelemetryService>>();

            var service = new TelemetryService(tractorTelemetryRepositoryMock.Object,
                                               combineTelemetryRepositoryMock.Object,
                                               tractorFilterValidator,
                                               combineFilterValidator,
                                               tractorSchemaRegistryMock.Object,
                                               combineSchemaRegistryMock.Object,
                                               loggerMock.Object);

            tractorSchemaRegistryMock.Setup(x => x.FieldExists(invalidFieldName)).Returns(false);
            combineSchemaRegistryMock.Setup(x => x.FieldExists(invalidFieldName)).Returns(false);

            // Act
            var ex = Assert.ThrowsAsync<FilterValidationException>(async () => await service.GetUnifiedTelemetriesAsync(paginatedFilter));

            // Assert
            StringAssert.Contains($"Field '{invalidFieldName}' is invalid in filter criteria", ex.Message);
        }

        [Test]
        public void GetUnifiedTelemetriesAsync_InvalidFilterOperation_ThrowsFilterValidationException()
        {
            // Arrange
            var dateTimeType = typeof(DateTime);
            var fieldName = "Date";
            var invalidOperation = FilterOperation.CONTAINS;
            var dateFilter = CreateFilterObject(fieldName, invalidOperation);
            var paginatedFilter = CreatePaginatedFilterObject(filters: [dateFilter]);

            var tractorTelemetryRepositoryMock = new Mock<ITractorTelemetryRepository>();
            var combineTelemetryRepositoryMock = new Mock<ICombineTelemetryRepository>();
            var tractorSchemaRegistryMock = new Mock<SchemaRegistry<TractorTelemetry>>();
            var combineSchemaRegistryMock = new Mock<SchemaRegistry<CombineTelemetry>>();
            var tractorFilterValidator = new FilterValidator<TractorTelemetry>(tractorSchemaRegistryMock.Object);
            var combineFilterValidator = new FilterValidator<CombineTelemetry>(combineSchemaRegistryMock.Object);
            var loggerMock = new Mock<ILogger<TelemetryService>>();

            var service = new TelemetryService(tractorTelemetryRepositoryMock.Object,
                                               combineTelemetryRepositoryMock.Object,
                                               tractorFilterValidator,
                                               combineFilterValidator,
                                               tractorSchemaRegistryMock.Object,
                                               combineSchemaRegistryMock.Object,
                                               loggerMock.Object);

            tractorSchemaRegistryMock.Setup(x => x.FieldExists(fieldName)).Returns(true);
            combineSchemaRegistryMock.Setup(x => x.FieldExists(fieldName)).Returns(true);

            tractorSchemaRegistryMock.Setup(x => x.TryGetFieldType(fieldName, out dateTimeType!)).Returns(true);
            combineSchemaRegistryMock.Setup(x => x.TryGetFieldType(fieldName, out dateTimeType!)).Returns(true);

            tractorSchemaRegistryMock.Setup(x => x.TypeHasAllowedOperation(dateTimeType, invalidOperation)).Returns(false);
            combineSchemaRegistryMock.Setup(x => x.TypeHasAllowedOperation(dateTimeType, invalidOperation)).Returns(false);

            // Act
            var ex = Assert.ThrowsAsync<FilterValidationException>(async () => await service.GetUnifiedTelemetriesAsync(paginatedFilter));

            // Assert
            StringAssert.Contains($"Operation '{invalidOperation}' is invalid for field '{fieldName}' in filter criteria", ex.Message);
        }

        [Test]
        public void GetUnifiedTelemetriesAsync_NullAssignmentToNonNullableField_ThrowsFilterValidationException()
        {
            // Arrange
            var dateTimeType = typeof(DateTime);
            var fieldName = "Date";
            var validOperation = FilterOperation.EQUALS;
            var dateFilter = CreateFilterObject(fieldName, validOperation);
            var paginatedFilter = CreatePaginatedFilterObject(filters: [dateFilter]);


            var tractorTelemetryRepositoryMock = new Mock<ITractorTelemetryRepository>();
            var combineTelemetryRepositoryMock = new Mock<ICombineTelemetryRepository>();
            var tractorSchemaRegistryMock = new Mock<SchemaRegistry<TractorTelemetry>>();
            var combineSchemaRegistryMock = new Mock<SchemaRegistry<CombineTelemetry>>();
            var tractorFilterValidator = new FilterValidator<TractorTelemetry>(tractorSchemaRegistryMock.Object);
            var combineFilterValidator = new FilterValidator<CombineTelemetry>(combineSchemaRegistryMock.Object);
            var loggerMock = new Mock<ILogger<TelemetryService>>();

            var service = new TelemetryService(tractorTelemetryRepositoryMock.Object,
                                               combineTelemetryRepositoryMock.Object,
                                               tractorFilterValidator,
                                               combineFilterValidator,
                                               tractorSchemaRegistryMock.Object,
                                               combineSchemaRegistryMock.Object,
                                               loggerMock.Object);

            tractorSchemaRegistryMock.Setup(x => x.FieldExists(fieldName)).Returns(true);
            combineSchemaRegistryMock.Setup(x => x.FieldExists(fieldName)).Returns(true);

            tractorSchemaRegistryMock.Setup(x => x.TryGetFieldType(fieldName, out dateTimeType!)).Returns(true);
            combineSchemaRegistryMock.Setup(x => x.TryGetFieldType(fieldName, out dateTimeType!)).Returns(true);

            tractorSchemaRegistryMock.Setup(x => x.TypeHasAllowedOperation(dateTimeType, validOperation)).Returns(true);
            combineSchemaRegistryMock.Setup(x => x.TypeHasAllowedOperation(dateTimeType, validOperation)).Returns(true);

            // Act
            var ex = Assert.ThrowsAsync<FilterValidationException>(async () => await service.GetUnifiedTelemetriesAsync(paginatedFilter));

            // Assert
            StringAssert.Contains($"NULL value cannot be assigned to non-nullable field '{fieldName}'", ex.Message);
        }

        [Test]
        public async Task GetUnifiedTelemetriesAsync_FilterWithSerialNumberAndDateFields_ReturnsUnifiedTelemetries()
        {
            // Arrange
            var dateTimeType = typeof(DateTime);
            var dateFieldName = "Date";
            var validOperation = FilterOperation.EQUALS;
            var dateValue = DateTime.Now;
            var dateFilter = CreateFilterObject(dateFieldName, validOperation, dateValue);

            var stringType = typeof(string);
            var serialNumberFieldName = "SerialNumber";
            var serialNumberValue = "ABC123";
            var serialNumberFilter = CreateFilterObject(serialNumberFieldName, validOperation, serialNumberValue);

            var paginatedFilter = CreatePaginatedFilterObject(filters: [serialNumberFilter, dateFilter]);
            var totalTractorTelemetryCount = 2;
            var totalCombineTelemetryCount = 1;
            var tractorTelemetryRepositoryMock = new Mock<ITractorTelemetryRepository>();
            var combineTelemetryRepositoryMock = new Mock<ICombineTelemetryRepository>();
            var tractorSchemaRegistryMock = new Mock<SchemaRegistry<TractorTelemetry>>();
            var combineSchemaRegistryMock = new Mock<SchemaRegistry<CombineTelemetry>>();
            var tractorFilterValidator = new FilterValidator<TractorTelemetry>(tractorSchemaRegistryMock.Object);
            var combineFilterValidator = new FilterValidator<CombineTelemetry>(combineSchemaRegistryMock.Object);
            var loggerMock = new Mock<ILogger<TelemetryService>>();

            var service = new TelemetryService(tractorTelemetryRepositoryMock.Object,
                                               combineTelemetryRepositoryMock.Object,
                                               tractorFilterValidator,
                                               combineFilterValidator,
                                               tractorSchemaRegistryMock.Object,
                                               combineSchemaRegistryMock.Object,
                                               loggerMock.Object);

            tractorSchemaRegistryMock.Setup(x => x.FieldExists(dateFieldName)).Returns(true);
            combineSchemaRegistryMock.Setup(x => x.FieldExists(dateFieldName)).Returns(true);
            tractorSchemaRegistryMock.Setup(x => x.FieldExists(serialNumberFieldName)).Returns(true);
            combineSchemaRegistryMock.Setup(x => x.FieldExists(serialNumberFieldName)).Returns(true);

            tractorSchemaRegistryMock.Setup(x => x.TryGetFieldType(dateFieldName, out dateTimeType!)).Returns(true);
            combineSchemaRegistryMock.Setup(x => x.TryGetFieldType(dateFieldName, out dateTimeType!)).Returns(true);
            tractorSchemaRegistryMock.Setup(x => x.TryGetFieldType(serialNumberFieldName, out stringType!)).Returns(true);
            combineSchemaRegistryMock.Setup(x => x.TryGetFieldType(serialNumberFieldName, out stringType!)).Returns(true);

            tractorSchemaRegistryMock.Setup(x => x.TypeHasAllowedOperation(dateTimeType, validOperation)).Returns(true);
            combineSchemaRegistryMock.Setup(x => x.TypeHasAllowedOperation(dateTimeType, validOperation)).Returns(true);
            tractorSchemaRegistryMock.Setup(x => x.TypeHasAllowedOperation(stringType, validOperation)).Returns(true);
            combineSchemaRegistryMock.Setup(x => x.TypeHasAllowedOperation(stringType, validOperation)).Returns(true);

            tractorTelemetryRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<TractorTelemetry, bool>>>(),
                                       It.IsAny<int>(),
                                       It.IsAny<int>(),
                                       It.IsAny<CancellationToken>()))
                .ReturnsAsync([new TractorTelemetry { }, new TractorTelemetry { }]);

            tractorTelemetryRepositoryMock
                .Setup(x => x.GetCountAsync(It.IsAny<Expression<Func<TractorTelemetry, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(totalTractorTelemetryCount);

            combineTelemetryRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<CombineTelemetry, bool>>>(),
                                       It.IsAny<int>(),
                                       It.IsAny<int>(),
                                       It.IsAny<CancellationToken>()))
                .ReturnsAsync([new CombineTelemetry { }]);

            combineTelemetryRepositoryMock
                .Setup(x => x.GetCountAsync(It.IsAny<Expression<Func<CombineTelemetry, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(totalCombineTelemetryCount);

            // Act
            var result = await service.GetUnifiedTelemetriesAsync(paginatedFilter);

            // Assert
            using var scope = new AssertionScope();
            Assert.That(result, Is.InstanceOf<UnifiedTelemetry>());
            Assert.Multiple(() =>
            {
                Assert.That(result.TractorTelemetry, Is.InstanceOf<IEnumerable<TractorTelemetry>>());
                Assert.That(result.TotalTractorsTelemetryCount, Is.EqualTo(totalTractorTelemetryCount));
                Assert.That(result.CombinesTelemetry, Is.InstanceOf<IEnumerable<CombineTelemetry>>());
                Assert.That(result.TotalCombinesTelemetryCount, Is.EqualTo(totalCombineTelemetryCount));
            });
        }

        // Continue writing unit test below
        // ...

        private PaginatedFilter CreatePaginatedFilterObject(int? pageNumber = null,
                                                            int? pageSize = null,
                                                            IEnumerable<Filter>? filters = null)
        {
            return new PaginatedFilter
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 10,
                Filters = filters ?? []
            };
        }

        private Filter CreateFilterObject(string? field = null, FilterOperation? operation = null, object? value = null)
        {
            return new Filter
            {
                Field = field ?? "SerialNumber",
                Operation = operation ?? FilterOperation.EQUALS,
                Value = value
            };
        }
    }
}
