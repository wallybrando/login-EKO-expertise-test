using LoginEKO.FileProcessingService.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Models
{
    public class UnifiedTelemetry
    {
        public int TotalTractorsTelemetryCount { get; set; }
        public IEnumerable<TractorTelemetry> TractorTelemetry { get; set; }

        public int TotalCombinesTelemetryCount { get; set; }
        public IEnumerable<CombineTelemetry> CombinesTelemetry { get; set; }
    }
}
