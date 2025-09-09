using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Models
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public required string Filename { get; set; }
        public required string Extension { get; set; }
        public long SizeInBytes { get; set; }
        public required byte[] BinaryObject { get; set; }
        public string MD5Hash { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
