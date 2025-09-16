using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Models
{
    public class Filter
    {
        public required string Field { get; set; }
        public required string Operation { get; set; }
        public required string? Value { get; set; }
    }
}
