using LoginEKO.FileProcessingService.Domain.Models.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Models
{
    public class FileMetadata : BaseModel
    {
        public required string Filename { get; set; }
        public required string Extension { get; set; }
        public long SizeInBytes { get; set; }
        public required byte[] BinaryObject { get; set; }
        public required IFormFile File { get; set; }
        public string MD5Hash { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
