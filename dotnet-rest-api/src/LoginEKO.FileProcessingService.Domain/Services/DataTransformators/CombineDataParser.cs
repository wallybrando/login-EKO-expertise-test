using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;

namespace LoginEKO.FileProcessingService.Domain.Services.DataTransformators
{
    public class CombineDataParser : IVehicleDataParser
    {
        public VehicleType Type { get; init; }
        private readonly IDictionary<string, string[]> _boolValues;
        
        public CombineDataParser()
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
        }

        public IEnumerable<Vehicle> TransformVehicleData(IEnumerable<string[]> data)
        {
            var combineTelemetry = new List<CombineTelemetry>();

            foreach (var entity in data)
            {
                var combine = new CombineTelemetry();

                try
                {
                    combine.Date = DataConverter.ToDateTime(entity[0]); // DateTime.Parse(entity[0]);
                    combine.SerialNumber = DataConverter.ToString(entity[1]);
                    combine.GPSLongitude = DataConverter.ToDouble(entity[2]); // double.Parse(entity[2]);
                    combine.GPSLatitude = DataConverter.ToDouble(entity[3]); //double.Parse(entity[3]);
                    combine.TotalWorkingHours = DataConverter.ToDouble(entity[4]); // double.Parse(entity[4]);
                    combine.GroundSpeedInKmh = DataConverter.ToDouble(entity[5]); //double.Parse(entity[5]);
                    combine.EngineSpeedInRpm = DataConverter.ToInt(entity[6]); // int.Parse(entity[6]);
                    combine.EngineLoadInPercentage = DataConverter.ToDouble(entity[7]); //double.Parse(entity[7]);
                    combine.DrumSpeedInRpm = DataConverter.ToInt(entity[8]); // int.Parse(entity[8]);
                    combine.FanSpeedInRpm = DataConverter.ToInt(entity[9]); // int.Parse(entity[9]);
                    combine.RotorStrawWalkerSpeedInRpm = DataConverter.ToInt(entity[10]); // int.Parse(entity[10]);
                    combine.SeparationLossesInPercentage = DataConverter.ToNullableDouble(entity[11]); //ParseNullablDoubleNumber(entity[11], nameof(CombineTelemetry.SeparationLossesInPercentages));  // entity[11] == "NA" ? null : double.Parse(entity[11]);
                    combine.SieveLossesInPercentage = DataConverter.ToNullableDouble(entity[12]); //entity[12] == "NA" ? null : double.Parse(entity[12]);
                    combine.Chopper = DataConverter.ToBool(entity[13],
                        _boolValues[nameof(CombineTelemetry.Chopper)][0],
                        _boolValues[nameof(CombineTelemetry.Chopper)][1]); //entity[13] == "on" ? true : false;
                    combine.DieselTankLevelInPercentage = DataConverter.ToDouble(entity[14]); // double.Parse(entity[14]);
                    combine.NumberOfPartialWidths = DataConverter.ToShort(entity[15]); // short.Parse(entity[15]);
                    combine.FrontAttachment = DataConverter.ToBool(entity[16],
                        _boolValues[nameof(CombineTelemetry.FrontAttachment)][0],
                        _boolValues[nameof(CombineTelemetry.FrontAttachment)][1]); // entity[16] == "on" ? true : false;
                    combine.MaxNumberOfPartialWidths = DataConverter.ToShort(entity[17]); //short.Parse(entity[17]);
                    combine.FeedRakeSpeedInRpm = DataConverter.ToInt(entity[18]); // int.Parse(entity[18]);
                    combine.WorkingPosition = DataConverter.ToBool(entity[19],
                        _boolValues[nameof(CombineTelemetry.WorkingPosition)][0],
                        _boolValues[nameof(CombineTelemetry.WorkingPosition)][1]); // entity[19] == "on" ? true : false;
                    combine.GrainTankUnloading = DataConverter.ToBool(entity[20],
                        _boolValues[nameof(CombineTelemetry.GrainTankUnloading)][0],
                        _boolValues[nameof(CombineTelemetry.GrainTankUnloading)][1]); // entity[20] == "on" ? true : false;
                    combine.MainDriveStatus = DataConverter.ToBool(entity[21],
                        _boolValues[nameof(CombineTelemetry.MainDriveStatus)][0],
                        _boolValues[nameof(CombineTelemetry.MainDriveStatus)][1]); //entity[21] == "on" ? true : false;
                    combine.ConcavePositionInMM = DataConverter.ToShort(entity[22]); //short.Parse(entity[22]);
                    combine.UpperSievePositionInMM = DataConverter.ToShort(entity[23]); // short.Parse(entity[23]);
                    combine.LowerSievePositionInMM = DataConverter.ToShort(entity[24]); // short.Parse(entity[24]);
                    combine.GrainTank70 = DataConverter.ToBool(entity[25],
                        _boolValues[nameof(CombineTelemetry.GrainTank70)][0],
                        _boolValues[nameof(CombineTelemetry.GrainTank70)][1]); //entity[25] == "on" ? true : false;
                    combine.GrainTank100 = DataConverter.ToBool(entity[26],
                        _boolValues[nameof(CombineTelemetry.GrainTank100)][0],
                        _boolValues[nameof(CombineTelemetry.GrainTank100)][1]); //entity[26] == "on" ? true : false;
                    combine.GrainMoistureContentInPercentage = DataConverter.ToNullableDouble(entity[27]); // entity[27] == "NA" ? null : double.Parse(entity[27]);
                    combine.ThroughputTonsPerHour = DataConverter.ToDouble(entity[28]); //double.Parse(entity[28]);
                    combine.RadialSpreaderSpeedInRpm = DataConverter.ToNullableInt(entity[29]); // entity[29] == "NA" ? null : int.Parse(entity[29]);
                    combine.GrainInReturnsInPercentage = DataConverter.ToDouble(entity[30]); // double.Parse(entity[30]);
                    combine.ChannelPositionInPercentage = DataConverter.ToDouble(entity[31]); //double.Parse(entity[31]);
                    combine.YieldMeasurement = DataConverter.ToBool(entity[32],
                        _boolValues[nameof(CombineTelemetry.GrainTank70)][0],
                        _boolValues[nameof(CombineTelemetry.GrainTank70)][1]); // entity[32] == "on" ? true : false;
                    combine.ReturnsAuferMeasurementInPercentage = DataConverter.ToDouble(entity[33]); // double.Parse(entity[33]);
                    combine.MoistureMeasurement = DataConverter.ToBool(entity[34],
                        _boolValues[nameof(CombineTelemetry.MoistureMeasurement)][0],
                        _boolValues[nameof(CombineTelemetry.MoistureMeasurement)][1]); //  entity[34] == "on" ? true : false;
                    combine.TypeOfCrop = DataConverter.ToEnum<CropType>(entity[35]); // ParseCropType(entity[35]); // CropType.SUNFLOWERS; // [35]
                    combine.SpecialCropWeightInGrams = DataConverter.ToInt(entity[36]); // int.Parse(entity[36]);
                    combine.AutoPilotStatus = DataConverter.ToBool(entity[37],
                        _boolValues[nameof(CombineTelemetry.AutoPilotStatus)][0],
                        _boolValues[nameof(CombineTelemetry.AutoPilotStatus)][1]); //  entity[34] == "on" ? true : false;
                    combine.CruisePilotStatus = DataConverter.ToEnum<CruisePilotStatus>(entity[38]); // ParseCruisePilotStatus(entity[38]); // CruisePilotStatus.STATUS_0; // [38]
                    combine.RateOfWorkInHaPerHour = DataConverter.ToDouble(entity[39]); // double.Parse(entity[39]);
                    combine.YieldInTonsPerHour = DataConverter.ToNullableDouble(entity[40]); // entity[40] == "NA" ? null : double.Parse(entity[40]);
                    combine.QuantimeterCalibrationFactor = DataConverter.ToDouble(entity[41]); // double.Parse(entity[41]);
                    combine.SeparationSensitivityInPercentage = DataConverter.ToDouble(entity[42]); // double.Parse(entity[42]);
                    combine.SieveSensitivityInPercentage = DataConverter.ToDouble(entity[43]); // double.Parse(entity[43]);
                }
                catch (Exception e)
                {

                    throw;
                }

                combineTelemetry.Add(combine);
            }

            return combineTelemetry;
        }
    }
}
