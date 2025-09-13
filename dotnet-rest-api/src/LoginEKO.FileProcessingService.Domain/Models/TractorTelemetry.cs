using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using System.ComponentModel;

namespace LoginEKO.FileProcessingService.Domain.Models
{
    public class TractorTelemetry : Vehicle
    {
        // Column G
        public double EngineLoadInPercentage { get; set; }
        // Column H
        public double? FuelConsumptionPerHour { get; set; }
        // Column I
        public double GroundSpeedGearboxInKmh { get; set; }
        // COlumn J
        public int? GroundSpeedRadarInKmh { get; set; }
        // Column K
        public int CoolantTemperatureInCelsius { get; set; }
        // Column L
        public int SpeedFrontPtoInRpm { get; set; }
        // COlumn M
        public int SpeedRearPtoInRpm { get; set; }
        // Column N
        public short? CurrentGearShift { get; set; }
        // Column O
        public double AmbientTemperatureInCelsius { get; set; }
        // Column P
        // see in other document what is here
        // Values 3
        public ParkingBreakStatus ParkingBreakStatus { get; set; }
        // Column Q
        // see in other document what is here
        // Values 0
        public TransverseDifferentialLockStatus TransverseDifferentialLockStatus { get; set; }
        // Column R
        // see in other document what is here
        // Values Active, Inactive, 2
        public WheelDriveStatus AllWheelDriveStatus { get; set; }
        // Column S
        // Values Active, Inactive, NA
        public bool? /*CreeperStatus?*/ ActualStatusOfCreeper { get; set; }
    }

    public enum CreeperStatus
    {
        [Description("Inactive")]
        INACTIVE = 0,
        [Description("Active")]
        ACTIVE = 1
    }

    //public enum WheelDriveStatus
    //{
    //    [Description("Inactive")]
    //    INACTIVE =  0,
    //    [Description("Active")]
    //    ACTIVE = 1,
    //    [Description("2")]
    //    STATUS_2 = 2
    //}

    //public enum TransverseDifferentialLockStatus
    //{
    //    [Description("0")]
    //    STATUS_0 = 0
    //}

    //public enum ParkingBreakStatus
    //{
    //    [Description("3")]
    //    STATUS_3 = 3
    //}
}
