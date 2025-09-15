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
                { nameof(CombineTelemetry.Chopper), ["on", "off"] },
                { nameof(CombineTelemetry.FrontAttachment), ["on", "off"] },
                { nameof(CombineTelemetry.WorkingPosition), ["on", "off"] },
                { nameof(CombineTelemetry.GrainTankUnloading), ["on", "off"] },
                { nameof(CombineTelemetry.MainDriveStatus), ["on", "off"] },
                { nameof(CombineTelemetry.GrainTank70), ["on", "off"] },
                { nameof(CombineTelemetry.GrainTank100), ["on", "off"] },
                { nameof(CombineTelemetry.MoistureMeasurement), ["on", "off"] },
                { nameof(CombineTelemetry.AutoPilotStatus), ["on", "off"] },
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
                    combine.Date = DataConverter.ToDateTime(entity[0]);
                    combine.SerialNumber = DataConverter.ToString(entity[1]);
                    combine.GPSLongitude = DataConverter.ToDouble(entity[2]);
                    combine.GPSLatitude = DataConverter.ToDouble(entity[3]);
                    combine.TotalWorkingHours = DataConverter.ToDouble(entity[4]);
                    combine.GroundSpeedInKmh = DataConverter.ToDouble(entity[5]);
                    combine.EngineSpeedInRpm = DataConverter.ToInt(entity[6]);
                    combine.EngineLoadInPercentage = DataConverter.ToDouble(entity[7]);
                    combine.DrumSpeedInRpm = DataConverter.ToInt(entity[8]);
                    combine.FanSpeedInRpm = DataConverter.ToInt(entity[9]); 
                    combine.RotorStrawWalkerSpeedInRpm = DataConverter.ToInt(entity[10]);
                    combine.SeparationLossesInPercentage = DataConverter.ToNullableDouble(entity[11]);
                    combine.SieveLossesInPercentage = DataConverter.ToNullableDouble(entity[12]);
                    combine.Chopper = DataConverter.ToBool(entity[13], _boolValues[nameof(CombineTelemetry.Chopper)][0], _boolValues[nameof(CombineTelemetry.Chopper)][1]);
                    combine.DieselTankLevelInPercentage = DataConverter.ToDouble(entity[14]);
                    combine.NumberOfPartialWidths = DataConverter.ToShort(entity[15]);
                    combine.FrontAttachment = DataConverter.ToBool(entity[16], _boolValues[nameof(CombineTelemetry.FrontAttachment)][0], _boolValues[nameof(CombineTelemetry.FrontAttachment)][1]);
                    combine.MaxNumberOfPartialWidths = DataConverter.ToShort(entity[17]);
                    combine.FeedRakeSpeedInRpm = DataConverter.ToInt(entity[18]);
                    combine.WorkingPosition = DataConverter.ToBool(entity[19], _boolValues[nameof(CombineTelemetry.WorkingPosition)][0], _boolValues[nameof(CombineTelemetry.WorkingPosition)][1]);
                    combine.GrainTankUnloading = DataConverter.ToBool(entity[20], _boolValues[nameof(CombineTelemetry.GrainTankUnloading)][0], _boolValues[nameof(CombineTelemetry.GrainTankUnloading)][1]);
                    combine.MainDriveStatus = DataConverter.ToBool(entity[21], _boolValues[nameof(CombineTelemetry.MainDriveStatus)][0], _boolValues[nameof(CombineTelemetry.MainDriveStatus)][1]);
                    combine.ConcavePositionInMM = DataConverter.ToShort(entity[22]);
                    combine.UpperSievePositionInMM = DataConverter.ToShort(entity[23]);
                    combine.LowerSievePositionInMM = DataConverter.ToShort(entity[24]);
                    combine.GrainTank70 = DataConverter.ToBool(entity[25], _boolValues[nameof(CombineTelemetry.GrainTank70)][0], _boolValues[nameof(CombineTelemetry.GrainTank70)][1]);
                    combine.GrainTank100 = DataConverter.ToBool(entity[26], _boolValues[nameof(CombineTelemetry.GrainTank100)][0], _boolValues[nameof(CombineTelemetry.GrainTank100)][1]);
                    combine.GrainMoistureContentInPercentage = DataConverter.ToNullableDouble(entity[27]);
                    combine.ThroughputTonsPerHour = DataConverter.ToDouble(entity[28]);
                    combine.RadialSpreaderSpeedInRpm = DataConverter.ToNullableInt(entity[29]);
                    combine.GrainInReturnsInPercentage = DataConverter.ToDouble(entity[30]);
                    combine.ChannelPositionInPercentage = DataConverter.ToDouble(entity[31]);
                    combine.YieldMeasurement = DataConverter.ToBool(entity[32], _boolValues[nameof(CombineTelemetry.GrainTank70)][0], _boolValues[nameof(CombineTelemetry.GrainTank70)][1]);
                    combine.ReturnsAugerMeasurementInPercentage = DataConverter.ToDouble(entity[33]);
                    combine.MoistureMeasurement = DataConverter.ToBool(entity[34], _boolValues[nameof(CombineTelemetry.MoistureMeasurement)][0], _boolValues[nameof(CombineTelemetry.MoistureMeasurement)][1]);
                    combine.TypeOfCrop = DataConverter.ToEnum<CropType>(entity[35]);
                    combine.SpecialCropWeightInGrams = DataConverter.ToInt(entity[36]);
                    combine.AutoPilotStatus = DataConverter.ToBool(entity[37], _boolValues[nameof(CombineTelemetry.AutoPilotStatus)][0], _boolValues[nameof(CombineTelemetry.AutoPilotStatus)][1]);
                    combine.CruisePilotStatus = DataConverter.ToEnum<CruisePilotStatus>(entity[38]);
                    combine.RateOfWorkInHaPerHour = DataConverter.ToDouble(entity[39]);
                    combine.YieldInTonsPerHour = DataConverter.ToNullableDouble(entity[40]);
                    combine.QuantimeterCalibrationFactor = DataConverter.ToDouble(entity[41]);
                    combine.SeparationSensitivityInPercentage = DataConverter.ToDouble(entity[42]);
                    combine.SieveSensitivityInPercentage = DataConverter.ToDouble(entity[43]);
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
