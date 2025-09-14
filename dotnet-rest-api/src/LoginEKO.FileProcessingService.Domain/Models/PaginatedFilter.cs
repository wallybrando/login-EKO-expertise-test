using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Models
{
    public class PaginatedFilter
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public IEnumerable<Filter> Filters { get; set; } = [];
    }
}
