using FluentAssertions.Execution;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Persistence.Database;
using LoginEKO.FileProcessingService.Persistence.Repositories;
using LoginEKO.FileProcessingService.Persistence.Tests.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace LoginEKO.FileProcessingService.Persistence.Tests.Repositories
{
    [TestFixture]
    public class TractorTelemetryRepositoryTests
    {
        private FileMetadata CreateFileMetadataObject(Guid? fileMetadataId = null, string? md5Hash = null)
        {
            return new FileMetadata
            {
                Id = fileMetadataId ?? Guid.NewGuid(),
                Extension = "csv",
                File = Mock.Of<IFormFile>(),
                Filename = $"tractorTelemetry_{Guid.NewGuid()}.csv",
                BinaryObject = new byte[16],
                MD5Hash = md5Hash ?? "md5Hash",
                SizeInBytes = 16,
                CreatedDate = DateTime.Now
            };
        }

        private TractorTelemetry CreateTractorTelemetryObject(Guid? fileMetadataId = null, DateTime? date = null)
        {
            return new TractorTelemetry
            {
                Date = date ?? DateTime.Now,
                FileMetadataId = fileMetadataId ?? Guid.NewGuid(),
                SerialNumber = "SerialNumber123",
                AllWheelDriveStatus = Domain.Models.Enums.WheelDriveStatus.ACTIVE
            };
        }

        private PaginatedFilter CreatePaginatedFilterObject(int? pageNumber = null, int? pageSize = null, IEnumerable<Filter>? filters = null)
        {
            var paginatedFilter = new PaginatedFilter
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 10,
            };
            if (filters != null)
                paginatedFilter.Filters = filters;

            return paginatedFilter;
        }

        private Filter CreateFilterObject(string? field = null, string? operation = null, string? value = null)
        {
            return new Filter
            {
                Field = field ?? "Date",
                Operation = operation ?? "Equals",
                Value = value ?? DateTime.Now.ToString()
            };
        }

        [Test]
        public async Task GetCountAsync_DateFilterApplied_ReturnTractorTelemetryCount()
        {
            // Given
            var expectedResult = 1;
            var date = DateTime.Now;
            var fileMetadataId = Guid.NewGuid();

            var fileMetadata = CreateFileMetadataObject(fileMetadataId);
            var tractorTelemetry = CreateTractorTelemetryObject(fileMetadataId, date);
            var filter = CreateFilterObject(value: date.ToString());
            var paginatedFilter = CreatePaginatedFilterObject(filters: new List<Filter> { filter });

            var context = MockDbContext.CreateInMemoryDbContext();
            context.FileMetadata.Add(fileMetadata);
            context.TractorTelemetries.Add(tractorTelemetry);
            context.SaveChanges();

            var filterExpressionBuilderMock = new Mock<IFilterExpressionBuilder<TractorTelemetry>>();
            filterExpressionBuilderMock.Setup(x => x.ApplyFilters(paginatedFilter.Filters)).Returns(new )
            var loggerMock = new Mock<ILogger<TractorTelemetryRepository>>();

            // When
            var repository = new TractorTelemetryRepository(context, filterExpressionBuilderMock.Object, loggerMock.Object);
            var result = await repository.GetCountAsync(paginatedFilter);

            // Then
            using var _ = new AssertionScope();
            Assert.Equals(expectedResult, result);
        }
    }
}
