using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;
using Microsoft.Extensions.Logging;

namespace LoginEKO.FileProcessingService.Domain.Services.DataTransformators
{
    public class TractorDataParser : IVehicleDataParser
    {
        private readonly ILogger<TractorDataParser> _logger;
        public VehicleType Type { get; init; }

        private readonly IDictionary<string, string[]> _boolValues;

        public TractorDataParser(ILogger<TractorDataParser> logger)
        {
            _logger = logger;
            Type = VehicleType.TRACTOR;

            _boolValues = new Dictionary<string, string[]>()
            {
                {nameof(TractorTelemetry.ActualStatusOfCreeper), new string[2] { "Active", "Inactive"} }
            };
        }

        public IEnumerable<Vehicle> TransformVehicleData(IEnumerable<string[]> data)
        {
            _logger.LogTrace("TransformVehicleData() data=tractor data collection");
            var tractorsTelemetry = new List<TractorTelemetry>();
            foreach (var entity in data)
            {
                var tractor = new TractorTelemetry();

                try
                {
                    tractor.Date = DataConverter.ToDateTime(entity[0]);
                    tractor.SerialNumber = DataConverter.ToString(entity[1]);
                    tractor.GPSLongitude = DataConverter.ToDouble(entity[2]);
                    tractor.GPSLatitude = DataConverter.ToDouble(entity[3]);
                    tractor.TotalWorkingHours = DataConverter.ToDouble(entity[4]);
                    tractor.EngineSpeedInRpm = DataConverter.ToInt(entity[5]); // int.Parse(entity[5]);
                    tractor.EngineLoadInPercentage = DataConverter.ToDouble(entity[6]);
                    tractor.FuelConsumptionPerHour = DataConverter.ToNullableDouble(entity[7]);
                    tractor.GroundSpeedGearboxInKmh = DataConverter.ToDouble(entity[8]);
                    tractor.GroundSpeedRadarInKmh = DataConverter.ToNullableInt(entity[9]);
                    tractor.CoolantTemperatureInCelsius = DataConverter.ToInt(entity[10]);
                    tractor.SpeedFrontPtoInRpm = DataConverter.ToInt(entity[11]);
                    tractor.SpeedRearPtoInRpm = DataConverter.ToInt(entity[12]);
                    tractor.CurrentGearShift = DataConverter.ToNullabeShort(entity[13]);
                    tractor.AmbientTemperatureInCelsius = DataConverter.ToDouble(entity[14]);
                    tractor.ParkingBreakStatus = DataConverter.ToEnum<ParkingBreakStatus>(entity[15]);
                    tractor.TransverseDifferentialLockStatus = DataConverter.ToEnum<TransverseDifferentialLockStatus>(entity[16]);
                    tractor.AllWheelDriveStatus = DataConverter.ToEnum<WheelDriveStatus>(entity[17]);
                    tractor.ActualStatusOfCreeper = DataConverter.ToNullableBool(entity[18],
                                                                           _boolValues[nameof(TractorTelemetry.ActualStatusOfCreeper)][0],
                                                                           _boolValues[nameof(TractorTelemetry.ActualStatusOfCreeper)][1]);
                }
                catch (Exception e)
                {
                    _logger.LogError("Failed to parse data");
                    throw;
                }

                tractorsTelemetry.Add(tractor);
            }

            _logger.LogDebug("Successfully transformed data to");
            return tractorsTelemetry;
        }
    }
}
