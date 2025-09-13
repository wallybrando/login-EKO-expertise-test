using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;

namespace LoginEKO.FileProcessingService.Domain.Services.DataTransformators
{
    public class TractorDataParser : IVehicleDataParser
    {
        public VehicleType Type { get; init; }

        private readonly IDictionary<string, string[]> _boolValues;

        public TractorDataParser()
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
                    tractor.Date = DataHelper.ConvertToDateTime(entity[0]);
                    tractor.SerialNumber = DataHelper.ConvertToString(entity[1]);
                    tractor.GPSLongitude = DataHelper.ConvertToDouble(entity[2]);
                    tractor.GPSLatitude = DataHelper.ConvertToDouble(entity[3]);
                    tractor.TotalWorkingHours = DataHelper.ConvertToDouble(entity[4]);
                    tractor.EngineSpeedInRpm = DataHelper.ConvertToInt(entity[5]); // int.Parse(entity[5]);
                    tractor.EngineLoadInPercentage = DataHelper.ConvertToDouble(entity[6]);
                    tractor.FuelConsumptionPerHour = DataHelper.ConvertToNullableDouble(entity[7]);
                    tractor.GroundSpeedGearboxInKmh = DataHelper.ConvertToDouble(entity[8]);
                    tractor.GroundSpeedRadarInKmh = DataHelper.ConvertToNullableInt(entity[9]);
                    tractor.CoolantTemperatureInCelsius = DataHelper.ConvertToInt(entity[10]);
                    tractor.SpeedFrontPtoInRpm = DataHelper.ConvertToInt(entity[11]);
                    tractor.SpeedRearPtoInRpm = DataHelper.ConvertToInt(entity[12]);
                    tractor.CurrentGearShift = DataHelper.ConvertToNullabeShort(entity[13]);
                    tractor.AmbientTemperatureInCelsius = DataHelper.ConvertToDouble(entity[14]);
                    tractor.ParkingBreakStatus = DataHelper.ParseEnumValue<ParkingBreakStatus>(entity[15]);
                    tractor.TransverseDifferentialLockStatus = DataHelper.ParseEnumValue<TransverseDifferentialLockStatus>(entity[16]);
                    tractor.AllWheelDriveStatus = DataHelper.ParseEnumValue<WheelDriveStatus>(entity[17]);
                    tractor.ActualStatusOfCreeper = DataHelper.ConvertToNullableBool(entity[18],
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

        //private int? ParseNullableIntValue(string field)
        //{
        //    if (field == "NA")
        //        return null;

        //    if (!int.TryParse(field, out var result))
        //        throw new ArgumentException("Value cannot be converted to int");

        //    return result;
        //}

        //private int ParseIntValue(string field)
        //{
        //    if (!int.TryParse(field, out var result))
        //        throw new ArgumentException("Value cannot be converted to int");

        //    return result;
        //}

        //private short? ParseNullabeShortValue(string field)
        //{
        //    if (field == "NA")
        //        return null;

        //    if (!short.TryParse(field, out var result))
        //        throw new ArgumentException("Value cannot be converted to short");

        //    return result;
        //}

 //       private bool? ParseNullableBoolValue(string field, string expectedTrue, string expectedFalse)
 //       {
 //           if (field == "NA")
 //               return null;
 //           if (field.Equals(expectedTrue, StringComparison.OrdinalIgnoreCase))
 //               return true;
 //           if (field.Equals(expectedFalse, StringComparison.OrdinalIgnoreCase))
 //               return false;

 //           throw new ArgumentException("Value cannot be coverted to bool");
 //       }

 //       private string ParseStringValue(string field)
 //       {
 //           if (string.IsNullOrEmpty(field))
 //               throw new ArgumentException("Value cannot be converted to string");

 //           return field;
 //       }

 //       private DateTime ParseDateTimeValue(string field)
 //       {
 //           if (!DateTime.TryParse(field, out var result))
 //               throw new ArgumentException("Value cannot be converted to DateTime");

 //           return result;
 //       }

 //       private double ParseDoubleValue(string columnValue)
 //       {
 //           if (!double.TryParse(columnValue, out var result))
 //               throw new ArgumentException("Value cannot be converted to double");

 //           return result;
 //       }

 //       private double? ParseNullableDoubleValue(string columnValue)
 //       {
 //           if (columnValue == "NA")
 //               return null;

 //           if (!double.TryParse(columnValue, out var result))
 //               throw new ArgumentException("Value cannot be converted to double");

 //           return result;
 //       }

 //       private static T ParseEnumValue<T>(string columnValue) where T : struct, Enum
 //       {
 //;
 //           if (!Enum.TryParse(typeof(T), columnValue, true, out var value))
 //               throw new ArgumentException("Value cannot be converted to enum");

 //           return (T)value; 
 //       }
    }
}
