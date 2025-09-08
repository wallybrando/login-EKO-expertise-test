using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Contracts.Requests
{
    public class UploadFileRequest
    {
        public required IFormFile File { get; init; }
    }
}
