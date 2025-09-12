using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;

namespace LoginEKO.FileProcessingService.Domain.Services.DataTransformators
{
    public class TractorDataTransformator : IVehicleDataTransformator
    {
        public VehicleType Type { get; init; }

        private readonly IDictionary<string, string[]> _boolValues;

        public TractorDataTransformator()
        {
            Type = VehicleType.TRACTOR;

            _boolValues = new Dictionary<string, string[]>()
            {
                {nameof(TractorTelemetry.ActualStatusOfCreeper), new string[2] { "Active", "Inactive"} }
            };
        }

        public IEnumerable<Vehicle> TransformVehicleData(IEnumerable<string[]> data)
        {
            var tractorsTelemetry = new List<TractorTelemetry>();
            foreach (var entity in data)
            {
                var tractor = new TractorTelemetry();

                try
                {
                    tractor.Date = ParseDateTimeValue(entity[0]);
                    tractor.SerialNumber = ParseStringValue(entity[1]);
                    tractor.GPSLongitude = ParseDoubleValue(entity[2]);
                    tractor.GPSLatitude = ParseDoubleValue(entity[3]);
                    tractor.TotalWorkingHours = ParseDoubleValue(entity[4]);
                    tractor.EngineSpeedInRpm = int.Parse(entity[5]);
                    tractor.EngineLoadInPercentage = ParseDoubleValue(entity[6]);
                    tractor.FuelConsumptionPerHour = ParseNullableDoubleValue(entity[7]);
                    tractor.GroundSpeedGearboxInKmh = ParseDoubleValue(entity[8]);
                    tractor.GroundSpeedRadarInKmh = ParseNullableIntValue(entity[9]);
                    tractor.CoolantTemperatureInCelsius = ParseIntValue(entity[10]);
                    tractor.SpeedFrontPtoInRpm = ParseIntValue(entity[11]);
                    tractor.SpeedRearPtoInRpm = ParseIntValue(entity[12]);
                    tractor.CurrentGearShift = ParseNullabeShortValue(entity[13]);
                    tractor.AmbientTemperatureInCelsius = ParseDoubleValue(entity[14]);
                    tractor.ParkingBreakStatus = ParseEnumValue<ParkingBreakStatus>(entity[15]);
                    tractor.TransverseDifferentialLockStatus = ParseEnumValue<TransverseDifferentialLockStatus>(entity[16]);
                    tractor.AllWheelDriveStatus = ParseEnumValue<WheelDriveStatus>(entity[17]);
                    tractor.ActualStatusOfCreeper = ParseNullableBoolValue(entity[18],
                                                                           _boolValues[nameof(TractorTelemetry.ActualStatusOfCreeper)][0],
                                                                           _boolValues[nameof(TractorTelemetry.ActualStatusOfCreeper)][1]);
                }
                catch (Exception e)
                {

                    throw;
                }

                tractorsTelemetry.Add(tractor);
            }

            return tractorsTelemetry;
        }

        private int? ParseNullableIntValue(string field)
        {
            if (field == "NA")
                return null;

            if (!int.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to int");

            return result;
        }

        private int ParseIntValue(string field)
        {
            if (!int.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to int");

            return result;
        }

        private short? ParseNullabeShortValue(string field)
        {
            if (field == "NA")
                return null;

            if (!short.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to short");

            return result;
        }

        private bool? ParseNullableBoolValue(string field, string expectedTrue, string expectedFalse)
        {
            if (field == "NA")
                return null;
            if (field.Equals(expectedTrue, StringComparison.OrdinalIgnoreCase))
                return true;
            if (field.Equals(expectedFalse, StringComparison.OrdinalIgnoreCase))
                return false;

            throw new ArgumentException("Value cannot be coverted to bool");
        }

        private string ParseStringValue(string field)
        {
            if (string.IsNullOrEmpty(field))
                throw new ArgumentException("Value cannot be converted to string");

            return field;
        }

        private DateTime ParseDateTimeValue(string field)
        {
            if (!DateTime.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to DateTime");

            return result;
        }

        private double ParseDoubleValue(string columnValue)
        {
            if (!double.TryParse(columnValue, out var result))
                throw new ArgumentException("Value cannot be converted to double");

            return result;
        }

        private double? ParseNullableDoubleValue(string columnValue)
        {
            if (columnValue == "NA")
                return null;

            if (!double.TryParse(columnValue, out var result))
                throw new ArgumentException("Value cannot be converted to double");

            return result;
        }

        private static T ParseEnumValue<T>(string columnValue) where T : struct, Enum
        {
 ;
            if (!Enum.TryParse(typeof(T), columnValue, true, out var value))
                throw new ArgumentException("Value cannot be converted to enum");

            return (T)value; 
        }
    }
}
