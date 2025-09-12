using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Models.Base
{
    public abstract class Vehicle : BaseModel
    {
        // Column B
        public string SerialNumber { get; set; } // common

        // Column A
        public DateTime Date { get; set; } // common
        // Column C
        // double from book reference
        public double GPSLongitude { get; set; } // common
        // Column D
        // double from book reference
        public double GPSLatitude { get; set; } // common
        // Column E
        public double TotalWorkingHours { get; set; } // common
        // Column F
        public int EngineSpeedInRpm { get; set; } // common
    }

    public abstract class BaseModel
    {
        public Guid Id { get; set; }
    }
}
