using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Contracts.Responses
{
    public class TelemetryResponse<T>
    {
        public required string Source { get; init; }
        public IEnumerable<T> Telemetry { get; init; } = [];
    }

    public class VehicleTelemetryResponses
    {
        public int? Page { get; init; }
        public int? PageSize { get; init; }
        public IEnumerable<TelemetryResponse<VehicleResponse>> Telemetry { get; init; } = [];
    }

    public class VehicleResponse
    {
        // Column B
        public string SerialNumber { get; init; } // common

        // Column A
        public string Date { get; init; } // common
        // Column C
        // double from book reference
        public string GPSLongitude { get; init; } // common
        // Column D
        // double from book reference
        public string GPSLatitude { get; init; } // common
        // Column E
        public string TotalWorkingHours { get; init; } // common
        // Column F
        public string EngineSpeedInRpm { get; init; } // common
    }

    public class TractorTelemetryResponse : VehicleResponse
    {
        // Column G
        public string EngineLoadInPercentage { get; init; }
        // Column H
        public string FuelConsumptionPerHour { get; init; }
        // Column I
        public string GroundSpeedGearboxInKmh { get; init; }
        // COlumn J
        public string GroundSpeedRadarInKmh { get; init; }
        // Column K
        public string CoolantTemperatureInCelsius { get; init; }
        // Column L
        public string SpeedFrontPtoInRpm { get; init; }
        // COlumn M
        public string SpeedRearPtoInRpm { get; init; }
        // Column N
        public string CurrentGearShift { get; init; }
        // Column O
        public string AmbientTemperatureInCelsius { get; init; }
        // Column P
        // see in other document what is here
        // Values 3
        public string ParkingBreakStatus { get; init; }
        // Column Q
        // see in other document what is here
        // Values 0
        public string TransverseDifferentialLockStatus { get; init; }
        // Column R
        // see in other document what is here
        // Values Active, Inactive, 2
        public string AllWheelDriveStatus { get; init; }
        // Column S
        // Values Active, Inactive, NA
        public string /*CreeperStatus?*/ ActualStatusOfCreeper { get; init; }
    }

    public class CombineTelemetryResponse : VehicleResponse
    {
        // Column F
        public string GroundSpeedInKmh { get; init; } // done
        // Column H
        public string EngineLoadInPercentage { get; init; } // done
        // Column I
        public string DrumSpeedInRpm { get; init; } // done
        // Column J
        public string FanSpeedInRpm { get; init; } // done
        // Column K
        public string RotorStrawWalkerSpeedInRpm { get; init; } // done
        // Column L
        public string SeparationLossesInPercentage { get; init; } // done
        // Column M
        public string SieveLossesInPercentage { get; init; } // done
        // Column N
        public string Chopper { get; init; } // done
        // Column O
        public string DieselTankLevelInPercentage { get; init; } // done
        // Column P
        public string NumberOfPartialWidths { get; init; } // done
        // Column Q
        public string FrontAttachment { get; init; } // done
        // Column R
        public string MaxNumberOfPartialWidths { get; init; } // done
        // Column S
        public string FeedRakeSpeedInRpm { get; init; } // done
        // Column T
        public string WorkingPosition { get; init; } // done
        // Column U
        public string GrainTankUnloading { get; init; } // done
        // Column V
        public string MainDriveStatus { get; init; } // done
        // Column W
        public string ConcavePositionInMM { get; init; } // done
        // Column X
        public string UpperSievePositionInMM { get; init; } // done
        // Column Y
        public string LowerSievePositionInMM { get; init; } // done
        // Column Z
        public string GrainTank70 { get; init; } // done
        // Column AA
        public string GrainTank100 { get; init; } // done
        // Column AB
        public string GrainMoistureContentInPercentage { get; init; } // done
        // Column AC
        public string ThroughputTonsPerHour { get; init; } // done
        // Column AD
        public string RadialSpreaderSpeedInRpm { get; init; } // done
        // Column AE
        public string GrainInReturnsInPercentage { get; init; } // done
        // Column AF
        public string ChannelPositionInPercentage { get; init; } // done
        // Column AG
        public string YieldMeasurement { get; init; } // done
        // Column AH
        public string ReturnsAugerMeasurementInPercentage { get; init; } // done
        // Column AI
        public string MoistureMeasurement { get; init; } // done
        // Column AJ
        public string TypeOfCrop { get; init; } // done
        // Column AK
        public string SpecialCropWeightInGrams { get; init; } // done
        // Column AL
        public string AutoPilotStatus { get; init; } // done
        // Column AM
        public string CruisePilotStatus { get; init; } // done
        // Column AN
        public string RateOfWorkInHaPerHour { get; init; } // done
        // Column AO
        public string? YieldInTonsPerHour { get; init; } // done
        // Column AP
        public string QuantimeterCalibrationFactor { get; init; } // done
        // Column AQ
        public string SeparationSensitivityInPercentage { get; init; } // done
        // Column AR
        public string SieveSensitivityInPercentage { get; init; }  // done
    }
}
