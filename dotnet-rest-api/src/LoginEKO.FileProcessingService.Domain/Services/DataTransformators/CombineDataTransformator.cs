using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LoginEKO.FileProcessingService.Domain.Extensions;

namespace LoginEKO.FileProcessingService.Domain.Services.DataTransformators
{
    public class CombineDataTransformator : IVehicleDataTransformator
    {
        public VehicleType Type { get; init; }
        private readonly Dictionary<string, string> _columnsWithSpecialValues;

        private readonly IDictionary<string, string[]> _boolValues;
        public CombineDataTransformator()
        {
            Type = VehicleType.COMBINE;

            _boolValues = new Dictionary<string, string[]>()
            {
                { nameof(CombineTelemetry.Chopper), new string[2] { "on", "off"} },
                { nameof(CombineTelemetry.FrontAttachment), new string[2] { "on", "off"} },
                { nameof(CombineTelemetry.WorkingPosition), new string[2] { "on", "off"} },
                { nameof(CombineTelemetry.GrainTankUnloading), new string[2] { "on", "off"} },
                { nameof(CombineTelemetry.MainDriveStatus), new string[2] { "on", "off"} },
                { nameof(CombineTelemetry.GrainTank70), new string[2] { "on", "off"} },
                { nameof(CombineTelemetry.GrainTank100), new string[2] { "on", "off"} },
                { nameof(CombineTelemetry.MoistureMeasurement), new string[2] { "on", "off"} },
                { nameof(CombineTelemetry.AutoPilotStatus), new string[2] { "on", "off"} },
            };

            //_columnsWithSpecialValues = new Dictionary<string, string>
            //{
            //    { nameof(CombineTelemetry.SeparationLossesInPercentages), "NA;" },
            //    { nameof(CombineTelemetry.SieveLossesInPercentage), "NA;" },
            //    { nameof(CombineTelemetry.Chopper), "on;off;" },

            //    { nameof(CombineTelemetry.FrontAttachment), "on;off;" },
            //    { nameof(CombineTelemetry.WorkingPosition), "on;off;" },
            //    { nameof(CombineTelemetry.GrainTankUnloading), "on;off;" },
            //    { nameof(CombineTelemetry.MainDriveStatus), "on;off;" },
            //    { nameof(CombineTelemetry.GrainTank70), "on;off;" },
            //    { nameof(CombineTelemetry.GrainTank100), "on;off;" },
            //    { nameof(CombineTelemetry.GrainMoistureContentInPercentage), "NA;" },
            //    { nameof(CombineTelemetry.RadialSpreaderSpeedInRpm), "NA;" },
            //    { nameof(CombineTelemetry.YieldMeasurement), "on;off;" },
            //    { nameof(CombineTelemetry.MoistureMeasurement), "on;off;" },
            //    { nameof(CombineTelemetry.TypeOfCrop), "Sunflowers;Maize;" },
            //    { nameof(CombineTelemetry.AutoPilotStatus), "on;off;" },
            //    { nameof(CombineTelemetry.CruisePilotStatus), "0;" },
            //    { nameof(CombineTelemetry.YieldInTonsPerHour), "NA;" },
            //};
        }

        public IEnumerable<Vehicle> TransformVehicleData(IEnumerable<string[]> data)
        {
            var combineTelemetry = new List<CombineTelemetry>();

            foreach (var entity in data)
            {
                var combine = new CombineTelemetry();

                try
                {
                    combine.Date = ParseDateTimeValue(entity[0]); // DateTime.Parse(entity[0]);
                    combine.SerialNumber = ParseStringValue(entity[1]);
                    combine.GPSLongitude = ParseDoubleValue(entity[2]); // double.Parse(entity[2]);
                    combine.GPSLatitude = ParseDoubleValue(entity[3]); //double.Parse(entity[3]);
                    combine.TotalWorkingHours = ParseDoubleValue(entity[4]); // double.Parse(entity[4]);
                    combine.GroundSpeedInKmh = ParseDoubleValue(entity[5]); //double.Parse(entity[5]);
                    combine.EngineSpeedInRpm = ParseIntValue(entity[6]); // int.Parse(entity[6]);
                    combine.EngineLoadInPercentage = ParseDoubleValue(entity[7]); //double.Parse(entity[7]);
                    combine.DrumSpeedInRpm = ParseIntValue(entity[8]); // int.Parse(entity[8]);
                    combine.FanSpeedInRpm = ParseIntValue(entity[9]); // int.Parse(entity[9]);
                    combine.RotorStrawWalkerSpeedInRpm = ParseIntValue(entity[10]); // int.Parse(entity[10]);
                    combine.SeparationLossesInPercentage = ParseNullableDoubleValue(entity[11]); //ParseNullablDoubleNumber(entity[11], nameof(CombineTelemetry.SeparationLossesInPercentages));  // entity[11] == "NA" ? null : double.Parse(entity[11]);
                    combine.SieveLossesInPercentage = ParseNullableDoubleValue(entity[12]); //entity[12] == "NA" ? null : double.Parse(entity[12]);
                    combine.Chopper = ParseBoolValue(entity[13],
                        _boolValues[nameof(CombineTelemetry.Chopper)][0],
                        _boolValues[nameof(CombineTelemetry.Chopper)][1]); //entity[13] == "on" ? true : false;
                    combine.DieselTankLevelInPercentage = ParseDoubleValue(entity[14]); // double.Parse(entity[14]);
                    combine.NumberOfPartialWidths = ParseShortValue(entity[15]); // short.Parse(entity[15]);
                    combine.FrontAttachment = ParseBoolValue(entity[16],
                        _boolValues[nameof(CombineTelemetry.FrontAttachment)][0],
                        _boolValues[nameof(CombineTelemetry.FrontAttachment)][1]); // entity[16] == "on" ? true : false;
                    combine.MaxNumberOfPartialWidths = ParseShortValue(entity[17]); //short.Parse(entity[17]);
                    combine.FeedRakeSpeedInRpm = ParseIntValue(entity[18]); // int.Parse(entity[18]);
                    combine.WorkingPosition = ParseBoolValue(entity[19],
                        _boolValues[nameof(CombineTelemetry.WorkingPosition)][0],
                        _boolValues[nameof(CombineTelemetry.WorkingPosition)][1]); // entity[19] == "on" ? true : false;
                    combine.GrainTankUnloading = ParseBoolValue(entity[20],
                        _boolValues[nameof(CombineTelemetry.GrainTankUnloading)][0],
                        _boolValues[nameof(CombineTelemetry.GrainTankUnloading)][1]); // entity[20] == "on" ? true : false;
                    combine.MainDriveStatus = ParseBoolValue(entity[21],
                        _boolValues[nameof(CombineTelemetry.MainDriveStatus)][0],
                        _boolValues[nameof(CombineTelemetry.MainDriveStatus)][1]); //entity[21] == "on" ? true : false;
                    combine.ConcavePositionInMM = ParseShortValue(entity[22]); //short.Parse(entity[22]);
                    combine.UpperSievePositionInMM = ParseShortValue(entity[23]); // short.Parse(entity[23]);
                    combine.LowerSievePositionInMM = ParseShortValue(entity[24]); // short.Parse(entity[24]);
                    combine.GrainTank70 = ParseBoolValue(entity[25],
                        _boolValues[nameof(CombineTelemetry.GrainTank70)][0],
                        _boolValues[nameof(CombineTelemetry.GrainTank70)][1]); //entity[25] == "on" ? true : false;
                    combine.GrainTank100 = ParseBoolValue(entity[26],
                        _boolValues[nameof(CombineTelemetry.GrainTank100)][0],
                        _boolValues[nameof(CombineTelemetry.GrainTank100)][1]); //entity[26] == "on" ? true : false;
                    combine.GrainMoistureContentInPercentage = ParseNullableDoubleValue(entity[27]); // entity[27] == "NA" ? null : double.Parse(entity[27]);
                    combine.ThroughputTonsPerHour = ParseDoubleValue(entity[28]); //double.Parse(entity[28]);
                    combine.RadialSpreaderSpeedInRpm = ParseNullableIntValue(entity[29]); // entity[29] == "NA" ? null : int.Parse(entity[29]);
                    combine.GrainInReturnsInPercentage = ParseDoubleValue(entity[30]); // double.Parse(entity[30]);
                    combine.ChannelPositionInPercentage = ParseDoubleValue(entity[31]); //double.Parse(entity[31]);
                    combine.YieldMeasurement = ParseBoolValue(entity[32],
                        _boolValues[nameof(CombineTelemetry.GrainTank70)][0],
                        _boolValues[nameof(CombineTelemetry.GrainTank70)][1]); // entity[32] == "on" ? true : false;
                    combine.ReturnsAuferMeasurementInPercentage = ParseDoubleValue(entity[33]); // double.Parse(entity[33]);
                    combine.MoistureMeasurement = ParseBoolValue(entity[34],
                        _boolValues[nameof(CombineTelemetry.MoistureMeasurement)][0],
                        _boolValues[nameof(CombineTelemetry.MoistureMeasurement)][1]); //  entity[34] == "on" ? true : false;
                    combine.TypeOfCrop = ParseEnumValue<CropType>(entity[35]); // ParseCropType(entity[35]); // CropType.SUNFLOWERS; // [35]
                    combine.SpecialCropWeightInGrams = ParseIntValue(entity[36]); // int.Parse(entity[36]);
                    combine.AutoPilotStatus = ParseBoolValue(entity[37],
                        _boolValues[nameof(CombineTelemetry.AutoPilotStatus)][0],
                        _boolValues[nameof(CombineTelemetry.AutoPilotStatus)][1]); //  entity[34] == "on" ? true : false;
                    combine.CruisePilotStatus = ParseEnumValue<CruisePilotStatus>(entity[38]); // ParseCruisePilotStatus(entity[38]); // CruisePilotStatus.STATUS_0; // [38]
                    combine.RateOfWorkInHaPerHour = ParseDoubleValue(entity[39]); // double.Parse(entity[39]);
                    combine.YieldInTonsPerHour = ParseNullableDoubleValue(entity[40]); // entity[40] == "NA" ? null : double.Parse(entity[40]);
                    combine.QuantimeterCalibrationFactor = ParseDoubleValue(entity[41]); // double.Parse(entity[41]);
                    combine.SeparationSensitivityInPercentage = ParseDoubleValue(entity[42]); // double.Parse(entity[42]);
                    combine.SieveSensitivityInPercentage = ParseDoubleValue(entity[43]); // double.Parse(entity[43]);
                }
                catch (Exception e)
                {

                    throw;
                }

                combineTelemetry.Add(combine);
            }

            //ParseNullableNumber("123");

            return combineTelemetry;
        }

        private int? ParseNullableIntValue(string field)
        {
            if (field == "NA")
                return null;

            return ParseIntValue(field);

            //if (!int.TryParse(field, out var result))
            //    throw new ArgumentException("Value cannot be converted to int");

            //return result;
        }

        private int ParseIntValue(string field)
        {
            if (!int.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to int");

            return result;
        }

        private short ParseShortValue(string field)
        {
            if (!short.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to short");

            return result;
        }

        private short? ParseNullabeShortValue(string field)
        {
            if (field == "NA")
                return null;

            return ParseShortValue(field);
        }

        private bool ParseBoolValue(string field, string expectedTrue, string expectedFalse)
        {
            if (field.Equals(expectedTrue, StringComparison.OrdinalIgnoreCase))
                return true;
            if (field.Equals(expectedFalse, StringComparison.OrdinalIgnoreCase))
                return false;

            throw new ArgumentException("Value cannot be coverted to bool");
        }

        private bool? ParseNullableBoolValue(string field, string expectedTrue, string expectedFalse)
        {
            if (field == "NA")
                return null;

            return ParseBoolValue(field, expectedTrue, expectedFalse);
            //if (field.Equals(expectedTrue, StringComparison.OrdinalIgnoreCase))
            //    return true;
            //if (field.Equals(expectedFalse, StringComparison.OrdinalIgnoreCase))
            //    return false;

            //throw new ArgumentException("Value cannot be coverted to bool");
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

        private double ParseDoubleValue(string field)
        {
            if (!double.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to double");

            return result;
        }

        private double? ParseNullableDoubleValue(string field)
        {
            if (field == "NA")
                return null;

            return ParseDoubleValue(field);
            //if (!double.TryParse(field, out var result))
            //    throw new ArgumentException("Value cannot be converted to double");

            //return result;
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
