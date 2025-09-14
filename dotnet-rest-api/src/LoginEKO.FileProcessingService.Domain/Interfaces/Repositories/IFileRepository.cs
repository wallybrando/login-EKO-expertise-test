using LoginEKO.FileProcessingService.Domain.Models;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Repositories
{
    public interface IFileRepository
    {
        Task<FileMetadata?> GetByMD5HashAsync(string md5Hash);
        Task<int> ImportFileAsync(FileMetadata file);
    }
}
