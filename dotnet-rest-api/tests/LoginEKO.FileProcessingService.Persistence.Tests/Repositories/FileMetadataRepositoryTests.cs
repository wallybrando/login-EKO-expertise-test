using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Persistence.Repositories;
using LoginEKO.FileProcessingService.Persistence.Tests.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace LoginEKO.FileProcessingService.Persistence.Tests.Repositories
{
    [TestFixture]
    public class FileMetadataRepositoryTests
    {
        [Test]
        public void ExistsByMD5HashAsync_InvaliddMD5Hash_ThrowsArgumentException()
        {
            // Arrange
            var md5Hash = "";

            var dbContext = MockDbContext.CreateInMemoryDbContext(Guid.NewGuid().ToString());
            var loggerMock = new Mock<ILogger<FileMetadataRepository>>();
            var repository = new FileMetadataRepository(dbContext, loggerMock.Object);

            // Act
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await repository.ExistsByMD5HashAsync(md5Hash)) ;

            // Assert
            StringAssert.Contains("MD5 Hash cannot be NULL or empty", ex.Message);
        }

        [Test]
        public async Task ExistsByMD5HashAsync_ValidMD5Hash_ReturnsFalse()
        {
            // Arrange
            var md5Hash = Guid.NewGuid().ToString();

            var dbContext = MockDbContext.CreateInMemoryDbContext(Guid.NewGuid().ToString());
            var loggerMock = new Mock<ILogger<FileMetadataRepository>>();
            var repository = new FileMetadataRepository(dbContext, loggerMock.Object);

            // Act
            var result = await repository.ExistsByMD5HashAsync(md5Hash);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task ExistsByMD5HashAsync_ValidMD5Hash_ReturnsTrue()
        {
            // Arrange
            var md5Hash = Guid.NewGuid().ToString();
            var fileMetadata = CreateObject(md5Hash: md5Hash);

            var dbContext = MockDbContext.CreateInMemoryDbContext(Guid.NewGuid().ToString());
            dbContext.FileMetadata.Add(fileMetadata);
            dbContext.SaveChanges();

            var loggerMock = new Mock<ILogger<FileMetadataRepository>>();
            var repository = new FileMetadataRepository(dbContext, loggerMock.Object);

            // Act
            var result = await repository.ExistsByMD5HashAsync(md5Hash);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ImportFileMetadataAsync_DatabaseCorrupted_ThrowsRepositoryException()
        {
            // Arrange
            var fileMetadata = CreateObject();

            var dbContext = MockDbContext.CreateInvalidInMemoryDbContext();
            var loggerMock = new Mock<ILogger<FileMetadataRepository>>();
            var repository = new FileMetadataRepository(dbContext, loggerMock.Object);

            // Act
            var ex = Assert.ThrowsAsync<RepositoryException>(async () => await repository.ImportFileMetadataAsync(fileMetadata)) ;

            // Assert
            StringAssert.Contains("Unexpected error occured in database", ex.Message);
        }

        [Test]
        public async Task ImportFileMetadataAsync_ValidFileMetadata_ReturnsOneAffectedRow()
        {
            // Arrange
            var fileMetadata = CreateObject();

            var dbContext = MockDbContext.CreateInMemoryDbContext(Guid.NewGuid().ToString());
            var loggerMock = new Mock<ILogger<FileMetadataRepository>>();
            var repository = new FileMetadataRepository(dbContext, loggerMock.Object);

            // Act
            var result = await repository.ImportFileMetadataAsync(fileMetadata);

            // Assert
            Assert.That(result,Is.EqualTo(1));
        }

        private FileMetadata CreateObject(Guid? Id = null, string? md5Hash = null)
        {
            return new FileMetadata
            {
                Id = Id ?? Guid.NewGuid(),
                MD5Hash = md5Hash ?? Guid.NewGuid().ToString(),
                SizeInBytes = 16,
                Filename = $"file_{DateTime.Now}",
                File = Mock.Of<IFormFile>(),
                CreatedDate = DateTime.Now,
                Extension = "csv",
                BinaryObject = new byte[16]
            };
        }
    }
}
