using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Contracts.Requests
{
    public class FilterRequest
    {
        public required string Field { get; init; }
        public string? Operation { get; init; }
        public required string? Value { get; init; }
    }
}
