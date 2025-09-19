using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class FileMetadataRepository : IFileMetadataRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<FileMetadataRepository> _logger;

        public FileMetadataRepository(ApplicationContext dbContext, ILogger<FileMetadataRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>Returns true if file exists in database based on MD5Hash, otherwise returns false</summary>
        public async Task<bool> ExistsByMD5HashAsync(string md5Hash, CancellationToken token = default)
        {
            _logger.LogTrace("GetByMD5HashAsync() md5Hash=******");
            if (string.IsNullOrEmpty(md5Hash))
            {
                _logger.LogError("MD5 Hash is not provided");
                throw new ArgumentException("MD5 Hash cannot be NULL or empty");
            }

            _logger.LogDebug("Attempting to get filemetadata with MD5Hash=****** from database");
            var entity = await _dbContext.FileMetadata.SingleOrDefaultAsync(x => x.MD5Hash == md5Hash, token);

            if (entity == null)
            {
                _logger.LogDebug("File does not exist in database with provided MD5 Hash");
                return false;
            }

            _logger.LogDebug("File exists in database with provided MD5 Hash");
            return true;
        }

        /// <summary>Imports file metadata into database</summary>
        public async Task<int> ImportFileMetadataAsync(FileMetadata file, CancellationToken token = default)
        {
            _logger.LogTrace("ImportFileAsync() file={Filename}", file.Filename);
            file.CreatedDate = DateTime.Now;

            try
            {
                _logger.LogDebug("Attempting to import telemetry data from file {Filename} to database", file.Filename);

                await _dbContext.FileMetadata.AddAsync(file, token);
                var result = await _dbContext.SaveChangesAsync(token);

                _logger.LogDebug("Successfully imported telemetry data to database");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to insert telemetry data. Message: {Message}", ex.Message);
                throw new RepositoryException("Unexpected error occured in database", ex);
            }
        }
    }
}
