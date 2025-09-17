using LoginEKO.FileProcessingService.Domain.Models.Entities.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;

namespace LoginEKO.FileProcessingService.Domain.Models.Entities
{
    public class CombineTelemetry : AgroVehicleTelemetry
    {
        public double GroundSpeedInKmh { get; set; }
        public double EngineLoadInPercentage { get; set; }
        public int DrumSpeedInRpm { get; set; }
        public int FanSpeedInRpm { get; set; }

        public int RotorStrawWalkerSpeedInRpm { get; set; }
        public double? SeparationLossesInPercentage { get; set; }
        public double? SieveLossesInPercentage { get; set; }
        public bool Chopper { get; set; }
        public double DieselTankLevelInPercentage { get; set; }
        public short NumberOfPartialWidths { get; set; }
        public bool FrontAttachment { get; set; }
        public short MaxNumberOfPartialWidths { get; set; }
        public int FeedRakeSpeedInRpm { get; set; }
        public bool WorkingPosition { get; set; }

        public bool GrainTankUnloading { get; set; }
        public bool MainDriveStatus { get; set; }
        public short ConcavePositionInMM { get; set; }
        public short UpperSievePositionInMM { get; set; }
        public short LowerSievePositionInMM { get; set; }
        public bool GrainTank70 { get; set; }
        public bool GrainTank100 { get; set; }
        public double? GrainMoistureContentInPercentage { get; set; }
        public double ThroughputTonsPerHour { get; set; }
        public int? RadialSpreaderSpeedInRpm { get; set; }

        public double GrainInReturnsInPercentage { get; set; }
        public double ChannelPositionInPercentage { get; set; }
        public bool YieldMeasurement { get; set; }
        public double ReturnsAugerMeasurementInPercentage { get; set; }
        public bool MoistureMeasurement { get; set; }
        public CropType TypeOfCrop { get; set; }
        public int SpecialCropWeightInGrams { get; set; }
        public bool AutoPilotStatus { get; set; } 
        public CruisePilotStatus CruisePilotStatus { get; set; }
        public double RateOfWorkInHaPerHour { get; set; }

        public double? YieldInTonsPerHour { get; set; }
        public double QuantimeterCalibrationFactor { get; set; }
        public double SeparationSensitivityInPercentage { get; set; } 
        public double SieveSensitivityInPercentage { get; set; }

        public Guid FileMetadataId { get; set; }
        public FileMetadata FileMetadata { get; set; }
    }
}
