using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Contracts.Responses
{
    public class FileResponse
    {
        public Guid Id { get; init; }
        public required string Filename { get; init; }
        public long SizeInBytes { get; init; }
        public DateTime CreatedDate { get; init; }
    }
}
