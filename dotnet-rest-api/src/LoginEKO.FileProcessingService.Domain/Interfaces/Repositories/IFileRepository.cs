using LoginEKO.FileProcessingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Repositories
{
    public interface IFileRepository
    {
        Task<FileMetadata?> GetByMD5HashAsync(string md5Hash);
        Task<bool> CreateFileMetadataAsync(FileMetadata file);
    }
}
