using LoginEKO.FileProcessingService.Domain.Models.Entities;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Repositories
{
    public interface IFileMetadataRepository
    {
        Task<bool> ExistsByMD5HashAsync(string md5Hash, CancellationToken token = default);
        Task<int> ImportFileMetadataAsync(FileMetadata file, CancellationToken token = default);
    }
}
