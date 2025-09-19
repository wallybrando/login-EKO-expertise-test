using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;

namespace LoginEKO.FileProcessingService.Domain.SchemaRegistries
{
    public class CombineTelemetrySchemaRegistry<T> : SchemaRegistry<T>
    {
        public CombineTelemetrySchemaRegistry()
        {
            FieldRegistry.Add(nameof(CombineTelemetry.EngineLoadInPercentage).ToLower(), typeof(double));
            FieldRegistry.Add(nameof(CombineTelemetry.GroundSpeedInKmh).ToLower(), typeof(double));
            FieldRegistry.Add(nameof(CombineTelemetry.DrumSpeedInRpm).ToLower(), typeof(int));
            FieldRegistry.Add(nameof(CombineTelemetry.FanSpeedInRpm).ToLower(), typeof(int));
            FieldRegistry.Add(nameof(CombineTelemetry.RotorStrawWalkerSpeedInRpm).ToLower(), typeof(int));
            FieldRegistry.Add(nameof(CombineTelemetry.SeparationLossesInPercentage).ToLower(), typeof(double?));
            FieldRegistry.Add(nameof(CombineTelemetry.SieveLossesInPercentage).ToLower(), typeof(double?));
            FieldRegistry.Add(nameof(CombineTelemetry.Chopper).ToLower(), typeof(bool));
            FieldRegistry.Add(nameof(CombineTelemetry.DieselTankLevelInPercentage).ToLower(), typeof(double));
            FieldRegistry.Add(nameof(CombineTelemetry.NumberOfPartialWidths).ToLower(), typeof(short));
            FieldRegistry.Add(nameof(CombineTelemetry.FrontAttachment).ToLower(), typeof(bool));
            FieldRegistry.Add(nameof(CombineTelemetry.MaxNumberOfPartialWidths).ToLower(), typeof(short));
            FieldRegistry.Add(nameof(CombineTelemetry.FeedRakeSpeedInRpm).ToLower(), typeof(int));
            FieldRegistry.Add(nameof(CombineTelemetry.WorkingPosition).ToLower(), typeof(bool));

            FieldRegistry.Add(nameof(CombineTelemetry.GrainTankUnloading).ToLower(), typeof(bool));
            FieldRegistry.Add(nameof(CombineTelemetry.MainDriveStatus).ToLower(), typeof(bool));
            FieldRegistry.Add(nameof(CombineTelemetry.ConcavePositionInMM).ToLower(), typeof(short));
            FieldRegistry.Add(nameof(CombineTelemetry.UpperSievePositionInMM).ToLower(), typeof(short));
            FieldRegistry.Add(nameof(CombineTelemetry.LowerSievePositionInMM).ToLower(), typeof(short));
            FieldRegistry.Add(nameof(CombineTelemetry.GrainTank70).ToLower(), typeof(bool));
            FieldRegistry.Add(nameof(CombineTelemetry.GrainTank100).ToLower(), typeof(bool));
            FieldRegistry.Add(nameof(CombineTelemetry.GrainMoistureContentInPercentage).ToLower(), typeof(double?));
            FieldRegistry.Add(nameof(CombineTelemetry.ThroughputTonsPerHour).ToLower(), typeof(double));
            FieldRegistry.Add(nameof(CombineTelemetry.RadialSpreaderSpeedInRpm).ToLower(), typeof(int?));

            FieldRegistry.Add(nameof(CombineTelemetry.GrainInReturnsInPercentage).ToLower(), typeof(double));
            FieldRegistry.Add(nameof(CombineTelemetry.ChannelPositionInPercentage).ToLower(), typeof(double));
            FieldRegistry.Add(nameof(CombineTelemetry.YieldMeasurement).ToLower(), typeof(bool));
            FieldRegistry.Add(nameof(CombineTelemetry.ReturnsAugerMeasurementInPercentage).ToLower(), typeof(double));
            FieldRegistry.Add(nameof(CombineTelemetry.MoistureMeasurement).ToLower(), typeof(bool));
            FieldRegistry.Add(nameof(CombineTelemetry.TypeOfCrop).ToLower(), typeof(Enum));
            FieldRegistry.Add(nameof(CombineTelemetry.SpecialCropWeightInGrams).ToLower(), typeof(int));
            FieldRegistry.Add(nameof(CombineTelemetry.AutoPilotStatus).ToLower(), typeof(bool));
            FieldRegistry.Add(nameof(CombineTelemetry.CruisePilotStatus).ToLower(), typeof(Enum));
            FieldRegistry.Add(nameof(CombineTelemetry.RateOfWorkInHaPerHour).ToLower(), typeof(double));

            FieldRegistry.Add(nameof(CombineTelemetry.YieldInTonsPerHour).ToLower(), typeof(double?));
            FieldRegistry.Add(nameof(CombineTelemetry.QuantimeterCalibrationFactor).ToLower(), typeof(double));
            FieldRegistry.Add(nameof(CombineTelemetry.SeparationSensitivityInPercentage).ToLower(), typeof(double));
            FieldRegistry.Add(nameof(CombineTelemetry.SieveSensitivityInPercentage).ToLower(), typeof(double));

            OperationRegistry.Add(typeof(CropType), FilterOperation.EQUALS);
            OperationRegistry.Add(typeof(CruisePilotStatus), FilterOperation.EQUALS);

            EnumRegistry = new Dictionary<string, Func<string, Enum>>
            {
                { nameof(CombineTelemetry.TypeOfCrop).ToLower(), x => DataConverter.ToEnum<CropType>(x) },
                { nameof(CombineTelemetry.CruisePilotStatus).ToLower(), x => DataConverter.ToEnum<CruisePilotStatus>(x) }
            };

            EnumTypeRegistry = new Dictionary<string, Type>
            {
                { nameof(CombineTelemetry.TypeOfCrop).ToLower(), typeof(CropType) },
                { nameof(CombineTelemetry.CruisePilotStatus).ToLower(), typeof(CruisePilotStatus) }
            };
        }
    }
}
