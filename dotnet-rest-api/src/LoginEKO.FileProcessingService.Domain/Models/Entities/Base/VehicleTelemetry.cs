namespace LoginEKO.FileProcessingService.Domain.Models.Entities.Base
{
    public abstract class VehicleTelemetry
    {
        public Guid Id { get; set; }
        public double TotalWorkingHours { get; set; }
        public int EngineSpeedInRpm { get; set; }
    }
}
