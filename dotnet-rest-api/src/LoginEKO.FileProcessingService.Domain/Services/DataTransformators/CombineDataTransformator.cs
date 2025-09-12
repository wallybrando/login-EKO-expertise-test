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
        public CombineDataTransformator()
        {
            Type = VehicleType.COMBINE;

            _columnsWithSpecialValues = new Dictionary<string, string>
            {
                { nameof(Combine.SeparationLossesInPercentages), "NA;" },
                { nameof(Combine.SieveLossesInPercentage), "NA;" },
                { nameof(Combine.Chopper), "on;off;" },

                { nameof(Combine.FrontAttachment), "on;off;" },
                { nameof(Combine.WorkingPosition), "on;off;" },
                { nameof(Combine.GrainTankUnloading), "on;off;" },
                { nameof(Combine.MainDriveStatus), "on;off;" },
                { nameof(Combine.GrainTank70), "on;off;" },
                { nameof(Combine.GrainTank100), "on;off;" },
                { nameof(Combine.GrainMoistureContentInPercentage), "NA;" },
                { nameof(Combine.RadialSpreaderSpeedInRpm), "NA;" },
                { nameof(Combine.YieldMeasurement), "on;off;" },
                { nameof(Combine.MoistureMeasurement), "on;off;" },
                { nameof(Combine.TypeOfCrop), "Sunflowers;Maize;" },
                { nameof(Combine.AutoPilotStatus), "on;off;" },
                { nameof(Combine.CruisePilotStatus), "0;" },
                { nameof(Combine.YieldInTonsPerHour), "NA;" },
            };
        }

        public IEnumerable<Vehicle> TransformVehicleData(IEnumerable<string[]> data)
        {
            var combineTelemetry = new List<Combine>();

            foreach (var entity in data)
            {
                var combine = new Combine();

                try
                {
                    combine.Date = DateTime.Parse(entity[0]);
                    combine.SerialNumber = entity[1];
                    combine.GPSLongitude = double.Parse(entity[2]);
                    combine.GPSLatitude = double.Parse(entity[3]);
                    combine.TotalWorkingHours = double.Parse(entity[4]);
                    combine.GroundSpeedInKmh = double.Parse(entity[5]);
                    combine.EngineSpeedInRpm = int.Parse(entity[6]);
                    combine.EngineLoadInPercentage = double.Parse(entity[7]);
                    combine.DrumSpeedInRpm = int.Parse(entity[8]);
                    combine.FanSpeedInRpm = int.Parse(entity[9]);
                    combine.RotorStrawWalkerSpeedInRpm = int.Parse(entity[10]);
                    combine.SeparationLossesInPercentages = ParseNullablDoubleNumber(entity[11], nameof(Combine.SeparationLossesInPercentages));  // entity[11] == "NA" ? null : double.Parse(entity[11]);
                    combine.SieveLossesInPercentage = entity[12] == "NA" ? null : double.Parse(entity[12]);
                    combine.Chopper = entity[13] == "on" ? true : false;
                    combine.DieselTankLevelInPercentage = double.Parse(entity[14]);
                    combine.NumberOfPartialWidths = short.Parse(entity[15]);
                    combine.FrontAttachment = entity[16] == "on" ? true : false;
                    combine.MaxNumberOfPartialWidths = short.Parse(entity[17]);
                    combine.FeedRakeSpeedInRpm = int.Parse(entity[18]);
                    combine.WorkingPosition = entity[19] == "on" ? true : false;
                    combine.GrainTankUnloading = entity[20] == "on" ? true : false;
                    combine.MainDriveStatus = entity[21] == "on" ? true : false;
                    combine.ConcavePositionInMM = short.Parse(entity[22]);
                    combine.UpperSievePositionInMM = short.Parse(entity[23]);
                    combine.LowerSievePositionInMM = short.Parse(entity[24]);
                    combine.GrainTank70 = entity[25] == "on" ? true : false;
                    combine.GrainTank100 = entity[26] == "on" ? true : false;
                    combine.GrainMoistureContentInPercentage = entity[27] == "NA" ? null : double.Parse(entity[27]);
                    combine.ThroughputTonsPerHour = double.Parse(entity[28]);
                    combine.RadialSpreaderSpeedInRpm = entity[29] == "NA" ? null : int.Parse(entity[29]);
                    combine.GrainInReturnsInPercentage = double.Parse(entity[30]);
                    combine.ChannelPositionInPercentage = double.Parse(entity[31]);
                    combine.YieldMeasurement = entity[32] == "on" ? true : false;
                    combine.ReturnsAuferMeasurementInPercentage = double.Parse(entity[33]);
                    combine.MoistureMeasurement = entity[34] == "on" ? true : false;
                    combine.TypeOfCrop = ParseCropType(entity[35]); // CropType.SUNFLOWERS; // [35]
                    combine.SpecialCropWeightInGrams = int.Parse(entity[36]);
                    combine.AutoPilotStatus = entity[37] == "on" ? true : false;
                    combine.CruisePilotStatus = ParseCruisePilotStatus(entity[38]); // CruisePilotStatus.STATUS_0; // [38]
                    combine.RateOfWorkInHaPerHour = double.Parse(entity[39]);
                    combine.YieldInTonsPerHour = entity[40] == "NA" ? null : double.Parse(entity[40]);
                    combine.QuantimeterCalibrationFactor = double.Parse(entity[41]);
                    combine.SeparationSensitivityInPercentage = double.Parse(entity[42]);
                    combine.SieveSensitivityInPercentage = double.Parse(entity[43]);
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

        private double ParseDoubleNumber(string number)
        {
            if (double.TryParse(number, out var parsedValue))
                return parsedValue;

            return 0;
        }

        private int ParseIntNumber(string number)
        {
            if (int.TryParse(number, out var parsedValue))
                return parsedValue;

            return 0;
        }

        private double? ParseNullablDoubleNumber(string number, string? columnKey = null)
        {
            if (columnKey != null && _columnsWithSpecialValues.TryGetValue(columnKey, out var val))
            {
                if (val.Split(';')[0] == number)
                    return null;
            }

            if (double.TryParse(number, out var parsedValue))
                return parsedValue;

            return null;
        }

        private int? ParseNullableIntNumber(string number, string? columnKey = null)
        {
            if (columnKey != null && _columnsWithSpecialValues.TryGetValue(columnKey, out var val))
            {
                if (val.Split(';')[0] == number)
                    return null;
            }

            if (int.TryParse(number, out var parsedValue))
                return parsedValue;

            return null;
        }

        private bool? ParseNullableBoolValue(string value, string columnKey)
        {
            if (_columnsWithSpecialValues.TryGetValue(columnKey, out var val))
            {
                if (val.Split(';')[0] == value)
                    return true;
                if (val.Split(';')[1] == value)
                    return false;
                return null;
            }

            return null;
        }

        private CropType ParseCropType(string value)
        {
            if (_columnsWithSpecialValues.TryGetValue(nameof(Combine.TypeOfCrop), out var val))
            {
                foreach (CropType e in Enum.GetValues(typeof(CropType)))
                {
                    if (e.GetDescription() == value)
                        return e;
                }
            }

            throw new ArgumentException();
        }

        private CruisePilotStatus ParseCruisePilotStatus(string value)
        {
            if (_columnsWithSpecialValues.TryGetValue(nameof(Combine.CruisePilotStatus), out var val))
            {
                foreach (CruisePilotStatus e in Enum.GetValues(typeof(CruisePilotStatus)))
                {
                    if (e.GetDescription() == value)
                        return e;
                }
            }

            throw new ArgumentException();
        }
    }
}
