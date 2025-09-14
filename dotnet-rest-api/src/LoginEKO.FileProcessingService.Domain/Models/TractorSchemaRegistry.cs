using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;

namespace LoginEKO.FileProcessingService.Domain.Models
{
    public class TractorSchemaRegistry : SchemaRegistry
    {
        public TractorSchemaRegistry()
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

            OperationRegistry = new Dictionary<Type, FilterOperation>
            {
                { typeof(string), FilterOperation.EQUALS | FilterOperation.CONTAINS },
                { typeof(DateTime), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan }, // tested
                { typeof(double), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(double?), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(int), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(int?), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(short), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(short?), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(bool), FilterOperation.EQUALS },
                { typeof(bool?), FilterOperation.EQUALS },
                { typeof(Enum), FilterOperation.EQUALS },
            };
        }
    }
}
