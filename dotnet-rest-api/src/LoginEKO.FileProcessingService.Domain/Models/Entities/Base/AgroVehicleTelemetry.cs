namespace LoginEKO.FileProcessingService.Domain.Models.Entities.Base
{
    public abstract class AgroVehicleTelemetry : VehicleTelemetry
    {
        public string SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public double GPSLongitude { get; set; }
        public double GPSLatitude { get; set; }
    }
}
