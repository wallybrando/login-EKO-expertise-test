using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Persistence.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LoginEKO.FileProcessingService.Persistence.Tests.Database
{
    public static class MockDbContext
    {
        public static ApplicationContext CreateInvalidInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().Options;

            var context = new ApplicationContext(options);

            return context;
        }

        public static ApplicationContext CreateInMemoryDbContext(string dbName = "testDB")
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(dbName).Options;

            var context = new ApplicationContext(options);

            SeedData(context);

            return context;
        }

        private static void SeedData(ApplicationContext context)
        {
            var tractorFileMetadaId = Guid.NewGuid();
            var combineFileMetadataId = Guid.NewGuid();

            context.FileMetadata.Add(new FileMetadata
            {
                Id = tractorFileMetadaId,
                Extension= "csv",
                File = Mock.Of<IFormFile>(),
                Filename="tractorTelemetry.csv",
                BinaryObject = new byte[16],
                MD5Hash = "Md5Hash1",
                SizeInBytes = 16,
                CreatedDate = DateTime.Now
            });
            context.FileMetadata.Add(new FileMetadata
            {
                Id = combineFileMetadataId,
                Extension = "csv",
                File = Mock.Of<IFormFile>(),
                Filename = "combineTelemetry.csv",
                BinaryObject = new byte[24],
                MD5Hash = "Md5Hash1",
                SizeInBytes = 16,
                CreatedDate = DateTime.Now
            });

            context.TractorTelemetries.Add(new TractorTelemetry
            {
                FileMetadataId = tractorFileMetadaId,
                SerialNumber = "SerialNumber123",
                AllWheelDriveStatus = Domain.Models.Enums.WheelDriveStatus.ACTIVE
            });
            context.TractorTelemetries.Add(new TractorTelemetry
            {
                FileMetadataId = tractorFileMetadaId,
                SerialNumber = "SerialNumber123",
                AllWheelDriveStatus = Domain.Models.Enums.WheelDriveStatus.INACTIVE
            });

            context.CombineTelemetries.Add(new CombineTelemetry
            {
                FileMetadataId = tractorFileMetadaId,
                SerialNumber = "SerialNumber123",
                TypeOfCrop = Domain.Models.Enums.CropType.MAIZE
            });
            context.CombineTelemetries.Add(new CombineTelemetry
            {
                FileMetadataId = tractorFileMetadaId,
                SerialNumber = "SerialNumber123",
                TypeOfCrop = Domain.Models.Enums.CropType.SUNFLOWERS
            });

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
