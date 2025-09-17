using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;

namespace LoginEKO.FileProcessingService.Persistence.SchemaRegistries
{
    public class TractorTelemetrySchemaRegistry<T> : SchemaRegistry<T>
    {
        public TractorTelemetrySchemaRegistry()
        {
            FieldRegistry = new Dictionary<string, Type>()
            {
                {  nameof(TractorTelemetry.SerialNumber).ToLower(), typeof(string) },
                {  nameof(TractorTelemetry.Date).ToLower(), typeof(DateTime) },
                {  nameof(TractorTelemetry.GPSLongitude).ToLower(), typeof(double) },
                {  nameof(TractorTelemetry.GPSLatitude).ToLower(), typeof(double) },
                {  nameof(TractorTelemetry.TotalWorkingHours).ToLower(), typeof(double) },
                {  nameof(TractorTelemetry.EngineSpeedInRpm).ToLower(), typeof(int) },
                {  nameof(TractorTelemetry.EngineLoadInPercentage).ToLower(), typeof(double) },
                {  nameof(TractorTelemetry.FuelConsumptionPerHour).ToLower(), typeof(double?) },
                {  nameof(TractorTelemetry.GroundSpeedGearboxInKmh).ToLower(), typeof(double) },
                {  nameof(TractorTelemetry.GroundSpeedRadarInKmh).ToLower(), typeof(int?) },
                {  nameof(TractorTelemetry.CoolantTemperatureInCelsius).ToLower(), typeof(int) },
                {  nameof(TractorTelemetry.SpeedFrontPtoInRpm).ToLower(), typeof(int) },
                {  nameof(TractorTelemetry.SpeedRearPtoInRpm).ToLower(), typeof(int) },
                {  nameof(TractorTelemetry.CurrentGearShift).ToLower(), typeof(short?) },
                {  nameof(TractorTelemetry.AmbientTemperatureInCelsius).ToLower(), typeof (double) },
                {  nameof(TractorTelemetry.ParkingBreakStatus).ToLower(), typeof(Enum) },
                {  nameof(TractorTelemetry.TransverseDifferentialLockStatus).ToLower(),typeof(Enum) },
                {  nameof(TractorTelemetry.AllWheelDriveStatus).ToLower(), typeof(Enum) },
                {  nameof(TractorTelemetry.ActualStatusOfCreeper).ToLower(), typeof(bool?) },
            };

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
