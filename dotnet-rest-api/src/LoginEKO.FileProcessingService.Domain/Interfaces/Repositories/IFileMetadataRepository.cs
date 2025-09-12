using LoginEKO.FileProcessingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Repositories
{
    public interface IFileMetadataRepository
    {
        Task<FileMetadata?> GetByMD5HashAsync(string md5Hash, IDbConnection connection);
        Task<bool> CreateFileMetadataAsync(FileMetadata file, IDbConnection? connection = null, IDbTransaction? transaction = null);
    }
}
