using LoginEKO.FileProcessingService.Domain.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces
{
    public interface ITextFileExtractor
    {
        FileType Type { get; init; }
        Task<IEnumerable<string[]>> ExtractDataAsync(IFormFile file);
    }
}
