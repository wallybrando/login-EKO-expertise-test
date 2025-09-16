using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Models.Base
{
    public abstract class Vehicle : BaseModel
    {
        public string SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public double GPSLongitude { get; set; }
        public double GPSLatitude { get; set; }
        public double TotalWorkingHours { get; set; }
        public int EngineSpeedInRpm { get; set; }
    }
}
