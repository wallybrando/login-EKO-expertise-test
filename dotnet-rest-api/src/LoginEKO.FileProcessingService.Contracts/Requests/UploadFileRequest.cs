using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Contracts.Requests
{
    public class UploadFileRequest
    {
        public required string Filename { get; init; }
        public double Size { get; init; }
        
    }
}
