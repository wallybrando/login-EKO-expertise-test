using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;

namespace LoginEKO.FileProcessingService.Domain.SchemaRegistries
{
    public class TractorTelemetrySchemaRegistry<T> : SchemaRegistry<T>
    {
        public TractorTelemetrySchemaRegistry()
        {
            FieldRegistry.Add(nameof(TractorTelemetry.EngineLoadInPercentage).ToLower(), typeof(double));
            FieldRegistry.Add(nameof(TractorTelemetry.FuelConsumptionPerHour).ToLower(), typeof(double?));
            FieldRegistry.Add(nameof(TractorTelemetry.GroundSpeedGearboxInKmh).ToLower(), typeof(double));
            FieldRegistry.Add(nameof(TractorTelemetry.GroundSpeedRadarInKmh).ToLower(), typeof(int?));
            FieldRegistry.Add(nameof(TractorTelemetry.CoolantTemperatureInCelsius).ToLower(), typeof(int));
            FieldRegistry.Add(nameof(TractorTelemetry.SpeedFrontPtoInRpm).ToLower(), typeof(int));
            FieldRegistry.Add(nameof(TractorTelemetry.SpeedRearPtoInRpm).ToLower(), typeof(int));
            FieldRegistry.Add(nameof(TractorTelemetry.CurrentGearShift).ToLower(), typeof(short?));
            FieldRegistry.Add(nameof(TractorTelemetry.AmbientTemperatureInCelsius).ToLower(), typeof(double));
            FieldRegistry.Add(nameof(TractorTelemetry.ParkingBreakStatus).ToLower(), typeof(Enum));
            FieldRegistry.Add(nameof(TractorTelemetry.TransverseDifferentialLockStatus).ToLower(), typeof(Enum));
            FieldRegistry.Add(nameof(TractorTelemetry.AllWheelDriveStatus).ToLower(), typeof(Enum));
            FieldRegistry.Add(nameof(TractorTelemetry.ActualStatusOfCreeper).ToLower(), typeof(bool?));


            OperationRegistry.Add(typeof(ParkingBreakStatus), FilterOperation.EQUALS);
            OperationRegistry.Add(typeof(TransverseDifferentialLockStatus), FilterOperation.EQUALS);
            OperationRegistry.Add(typeof(WheelDriveStatus), FilterOperation.EQUALS);

            EnumRegistry = new Dictionary<string, Func<string, Enum>>
            {
                { nameof(TractorTelemetry.ParkingBreakStatus).ToLower(), x => DataConverter.ToEnum<ParkingBreakStatus>(x) },
                { nameof(TractorTelemetry.TransverseDifferentialLockStatus).ToLower(), x => DataConverter.ToEnum<TransverseDifferentialLockStatus>(x) },
                { nameof(TractorTelemetry.AllWheelDriveStatus).ToLower(), x => DataConverter.ToEnum<WheelDriveStatus>(x) }
            };

            EnumTypeRegistry = new Dictionary<string, Type>
            {
                { nameof(TractorTelemetry.ParkingBreakStatus).ToLower(), typeof(ParkingBreakStatus) },
                { nameof(TractorTelemetry.TransverseDifferentialLockStatus).ToLower(), typeof(TransverseDifferentialLockStatus) },
                { nameof(TractorTelemetry.AllWheelDriveStatus).ToLower(), typeof(WheelDriveStatus) }
            };
        }
    }
}
