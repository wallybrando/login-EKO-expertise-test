using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly List<FileDto> _files = [];
        public Task<Guid> UploadFileAsync(FileDto file)
        {
            _files.Add(file);
            return Task.FromResult(file.Id);
        }
    }
}
