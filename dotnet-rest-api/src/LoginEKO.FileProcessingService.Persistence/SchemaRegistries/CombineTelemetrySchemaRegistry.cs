using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;

namespace LoginEKO.FileProcessingService.Persistence.SchemaRegistries
{
    public class CombineTelemetrySchemaRegistry<T> : SchemaRegistry<T>
    {
        public CombineTelemetrySchemaRegistry()
        {
            FieldRegistry = new Dictionary<string, Type>()
            {
                {  nameof(CombineTelemetry.SerialNumber).ToLower(), typeof(string) },
                {  nameof(CombineTelemetry.Date).ToLower(), typeof(DateTime) },
                {  nameof(CombineTelemetry.GPSLongitude).ToLower(), typeof(double) },
                {  nameof(CombineTelemetry.GPSLatitude).ToLower(), typeof(double) },
                {  nameof(CombineTelemetry.TotalWorkingHours).ToLower(), typeof(double) },
                {  nameof(CombineTelemetry.EngineSpeedInRpm).ToLower(), typeof(int) },
                {  nameof(CombineTelemetry.EngineLoadInPercentage).ToLower(), typeof(double) },
                {  nameof(CombineTelemetry.GroundSpeedInKmh).ToLower(), typeof(double) },
                {  nameof(CombineTelemetry.DrumSpeedInRpm).ToLower(), typeof(int) },
                {  nameof(CombineTelemetry.FanSpeedInRpm).ToLower(), typeof(int) },

                {  nameof(CombineTelemetry.RotorStrawWalkerSpeedInRpm).ToLower(), typeof(int) },
                {  nameof(CombineTelemetry.SeparationLossesInPercentage).ToLower(), typeof(double?) },
                {  nameof(CombineTelemetry.SieveLossesInPercentage).ToLower(), typeof(double?) },
                {  nameof(CombineTelemetry.Chopper).ToLower(), typeof(bool) },
                {  nameof(CombineTelemetry.DieselTankLevelInPercentage).ToLower(), typeof(double) },
                {  nameof(CombineTelemetry.NumberOfPartialWidths).ToLower(), typeof(short) },
                {  nameof(CombineTelemetry.FrontAttachment).ToLower(), typeof(bool) },
                {  nameof(CombineTelemetry.MaxNumberOfPartialWidths).ToLower(), typeof(short) },
                {  nameof(CombineTelemetry.FeedRakeSpeedInRpm).ToLower(), typeof(int) },
                {  nameof(CombineTelemetry.WorkingPosition).ToLower(), typeof(bool) },

                {  nameof(CombineTelemetry.GrainTankUnloading).ToLower(), typeof(bool) },
                {  nameof(CombineTelemetry.MainDriveStatus).ToLower(), typeof(bool) },
                {  nameof(CombineTelemetry.ConcavePositionInMM).ToLower(), typeof(short) },
                {  nameof(CombineTelemetry.UpperSievePositionInMM).ToLower(), typeof(short) },
                {  nameof(CombineTelemetry.LowerSievePositionInMM).ToLower(), typeof(short) },
                {  nameof(CombineTelemetry.GrainTank70).ToLower(), typeof(bool) },
                {  nameof(CombineTelemetry.GrainTank100).ToLower(), typeof(bool) },
                {  nameof(CombineTelemetry.GrainMoistureContentInPercentage).ToLower(), typeof(double?) },
                {  nameof(CombineTelemetry.ThroughputTonsPerHour).ToLower(), typeof(double) },
                {  nameof(CombineTelemetry.RadialSpreaderSpeedInRpm).ToLower(), typeof(int?) },

                {  nameof(CombineTelemetry.GrainInReturnsInPercentage).ToLower(), typeof(double) },
                {  nameof(CombineTelemetry.ChannelPositionInPercentage).ToLower(), typeof(double) },
                {  nameof(CombineTelemetry.YieldMeasurement).ToLower(), typeof(bool) },
                {  nameof(CombineTelemetry.ReturnsAugerMeasurementInPercentage).ToLower(), typeof(double) },
                {  nameof(CombineTelemetry.MoistureMeasurement).ToLower(), typeof(bool) },
                {  nameof(CombineTelemetry.TypeOfCrop).ToLower(), typeof(Enum) },
                {  nameof(CombineTelemetry.SpecialCropWeightInGrams).ToLower(), typeof(int) },
                {  nameof(CombineTelemetry.AutoPilotStatus).ToLower(), typeof(bool) },
                {  nameof(CombineTelemetry.CruisePilotStatus).ToLower(), typeof(Enum) },
                {  nameof(CombineTelemetry.RateOfWorkInHaPerHour).ToLower(), typeof(double) },

                {  nameof(CombineTelemetry.YieldInTonsPerHour).ToLower(), typeof(double?) },
                {  nameof(CombineTelemetry.QuantimeterCalibrationFactor).ToLower(), typeof(double) },
                {  nameof(CombineTelemetry.SeparationSensitivityInPercentage).ToLower(), typeof(double) },
                {  nameof(CombineTelemetry.SieveSensitivityInPercentage).ToLower(), typeof(double) }
            };

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
