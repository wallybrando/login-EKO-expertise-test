using LoginEKO.FileProcessingService.Domain.Models.Base;
using System.ComponentModel;

namespace LoginEKO.FileProcessingService.Domain.Models
{
    public class CombineTelemetry : Vehicle
    {
        // Column B
        //public string SerialNumber { get; set; } // done

        // Column A
        //public DateTime Date { get; set; } // done
        // Column C
        // double from book reference
        // double GPSLongitude { get; set; } // done
        // Column D
        // double from book reference
        //public double GPSLatitude { get; set; } // done
        // Column E
        // double TotalWorkingHours { get; set; } // done
        // Column F
        public double GroundSpeedInKmh { get; set; } // done
        // Column G
        //public int EngineSpeedInRpm { get; set; } // done
        // Column H
        public double EngineLoadInPercentage { get; set; } // done
        // Column I
        public int DrumSpeedInRpm { get; set; } // done
        // Column J
        public int FanSpeedInRpm { get; set; } // done
        // Column K
        public int RotorStrawWalkerSpeedInRpm { get; set; } // done
        // Column L
        public double? SeparationLossesInPercentage { get; set; } // done
        // Column M
        public double? SieveLossesInPercentage { get; set; } // done
        // Column N
        public bool Chopper { get; set; } // done
        // Column O
        public double DieselTankLevelInPercentage { get; set; } // done
        // Column P
        public short NumberOfPartialWidths { get; set; } // done
        // Column Q
        public bool FrontAttachment { get; set; } // done
        // Column R
        public short MaxNumberOfPartialWidths { get; set; } // done
        // Column S
        public int FeedRakeSpeedInRpm { get; set; } // done
        // Column T
        public bool WorkingPosition { get; set; } // done
        // Column U
        public bool GrainTankUnloading { get; set; } // done
        // Column V
        public bool MainDriveStatus { get; set; } // done
        // Column W
        public short ConcavePositionInMM { get; set; } // done
        // Column X
        public short UpperSievePositionInMM { get; set; } // done
        // Column Y
        public short LowerSievePositionInMM { get; set; } // done
        // Column Z
        public bool GrainTank70 { get; set; } // done
        // Column AA
        public bool GrainTank100 { get; set; } // done
        // Column AB
        public double? GrainMoistureContentInPercentage { get; set; } // done
        // Column AC
        public double ThroughputTonsPerHour { get; set; } // done
        // Column AD
        public int? RadialSpreaderSpeedInRpm { get; set; } // done
        // Column AE
        public double GrainInReturnsInPercentage { get; set; } // done
        // Column AF
        public double ChannelPositionInPercentage { get; set; } // done
        // Column AG
        public bool YieldMeasurement { get; set; } // done
        // Column AH
        public double ReturnsAuferMeasurementInPercentage { get; set; } // done
        
        
        
        // Column AI
        public bool MoistureMeasurement { get; set; } // done
        // Column AJ
        public CropType TypeOfCrop { get; set; } // done
        // Column AK
        public int SpecialCropWeightInGrams { get; set; } // done
        // Column AL
        public bool AutoPilotStatus { get; set; } // done
        // Column AM
        public CruisePilotStatus CruisePilotStatus { get; set; } // done
        // Column AN
        public double RateOfWorkInHaPerHour { get; set; } // done
        // Column AO
        public double? YieldInTonsPerHour { get; set; } // done
        // Column AP
        public double QuantimeterCalibrationFactor { get; set; } // done
        // Column AQ
        public double SeparationSensitivityInPercentage { get; set; } // done
        // Column AR
        public double SieveSensitivityInPercentage { get; set; }  // done
    }

    public enum CropType
    {
        [Description("Maize")]
        MAIZE = 1,
        [Description("Sunflowers")]
        SUNFLOWERS = 2
    }

    public enum CruisePilotStatus
    {
        [Description("0")]
        STATUS_0 = 0
    }
}
