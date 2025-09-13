using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationContext _dbContext;

        public FileRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FileMetadata?> GetByMD5HashAsync(string md5Hash)
        {
            if (string.IsNullOrEmpty(md5Hash))
                throw new ArgumentException();

            var entity = await _dbContext.FileMetadata.SingleOrDefaultAsync(x => x.MD5Hash == md5Hash);

            return entity;
        }

        public async Task<bool> CreateFileMetadataAsync(FileMetadata file)
        {
            file.CreatedDate = DateTime.Now;
            _dbContext.FileMetadata.Add(file);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
