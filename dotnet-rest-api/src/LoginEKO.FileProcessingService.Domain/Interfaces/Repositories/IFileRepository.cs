using LoginEKO.FileProcessingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Repositories
{
    public interface IFileRepository
    {
        Task<bool> UploadFileAsync(FileDto file);
        Task<FileDto?> GetByIdAsync(Guid id);
        Task<FileDto?> GetByFilenameAsync(string filename);
    }
}
