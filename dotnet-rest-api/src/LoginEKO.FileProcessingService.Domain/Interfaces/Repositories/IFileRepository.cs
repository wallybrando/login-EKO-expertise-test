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
        Task<Guid> UploadFileAsync(FileDto file);
        Task<FileDto?> GetAsync(Guid id);
    }
}
