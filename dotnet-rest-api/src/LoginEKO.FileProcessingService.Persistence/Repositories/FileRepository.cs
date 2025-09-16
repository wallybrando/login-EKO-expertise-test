using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<FileRepository> _logger;

        public FileRepository(ApplicationContext dbContext, ILogger<FileRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<FileMetadata?> GetByMD5HashAsync(string md5Hash, CancellationToken token = default)
        {
            _logger.LogTrace("GetByMD5HashAsync() md5Hash=******");
            if (string.IsNullOrEmpty(md5Hash))
            {
                _logger.LogError("MD5 Hash is not provided");
                throw new ArgumentException("MD5 Hash is empty");
            }

            _logger.LogDebug("Attempting to get filemetadata with MD5Hash=****** from database");
            var entity = await _dbContext.FileMetadata.SingleOrDefaultAsync(x => x.MD5Hash == md5Hash, token);

            _logger.LogDebug("Successfully get filemetadata from database");
            return entity;
        }

        public async Task<int> ImportFileAsync(FileMetadata file, CancellationToken token = default)
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
