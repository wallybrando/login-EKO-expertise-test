namespace LoginEKO.FileProcessingService.Domain.Models.Base
{
    public abstract class VehicleTelemetry : BaseModel
    {
        public string SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public double GPSLongitude { get; set; }
        public double GPSLatitude { get; set; }
        public double TotalWorkingHours { get; set; }
        public int EngineSpeedInRpm { get; set; }
    }
}
