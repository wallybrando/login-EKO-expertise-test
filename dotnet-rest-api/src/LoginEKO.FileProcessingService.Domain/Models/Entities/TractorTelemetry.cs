using LoginEKO.FileProcessingService.Domain.Models.Entities.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using System.ComponentModel;

namespace LoginEKO.FileProcessingService.Domain.Models.Entities
{
    public class TractorTelemetry : AgroVehicleTelemetry
    {
        public double EngineLoadInPercentage { get; set; }
        public double? FuelConsumptionPerHour { get; set; }
        public double GroundSpeedGearboxInKmh { get; set; }
        public int? GroundSpeedRadarInKmh { get; set; }
        public int CoolantTemperatureInCelsius { get; set; }
        public int SpeedFrontPtoInRpm { get; set; }
        public int SpeedRearPtoInRpm { get; set; }
        public short? CurrentGearShift { get; set; }
        public double AmbientTemperatureInCelsius { get; set; }
        public ParkingBreakStatus ParkingBreakStatus { get; set; }
        public TransverseDifferentialLockStatus TransverseDifferentialLockStatus { get; set; }
        // Values Active, Inactive, 2
        public WheelDriveStatus AllWheelDriveStatus { get; set; }
        // Values Active, Inactive, NA
        public bool?  ActualStatusOfCreeper { get; set; }

        public Guid FileMetadataId { get; set; }
        public FileMetadata FileMetadata { get; set; }
    }

    public enum CreeperStatus
    {
        [Description("Inactive")]
        INACTIVE = 0,
        [Description("Active")]
        ACTIVE = 1
    }
}
