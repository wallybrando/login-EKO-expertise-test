using LoginEKO.FileProcessingService.Domain.Extensions;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;

namespace LoginEKO.FileProcessingService.Domain.Services.DataTransformators
{
    public class TractorDataTransformator : IVehicleDataTransformator
    {
        public VehicleType Type { get; init; }
        private readonly Dictionary<string, string> _columnsWithSpecialValues;

        public TractorDataTransformator()
        {
            Type = VehicleType.TRACTOR;

            _columnsWithSpecialValues = new Dictionary<string, string>
            {
                { nameof(TractorTelemetry.ParkingBreakStatus), "3;" },
                { nameof(TractorTelemetry.TransverseDifferentialLockStatus), "0;" },
                { nameof(TractorTelemetry.AllWheelDriveStatus), "Active;Inactive;2" },
                //{ nameof(Combine.SieveLossesInPercentage), "NA;" },
                //{ nameof(Combine.Chopper), "on;off;" },

                //{ nameof(Combine.FrontAttachment), "on;off;" },
                //{ nameof(Combine.WorkingPosition), "on;off;" },
                //{ nameof(Combine.GrainTankUnloading), "on;off;" },
                //{ nameof(Combine.MainDriveStatus), "on;off;" },
                //{ nameof(Combine.GrainTank70), "on;off;" },
                //{ nameof(Combine.GrainTank100), "on;off;" },
                //{ nameof(Combine.GrainMoistureContentInPercentage), "NA;" },
                //{ nameof(Combine.RadialSpreaderSpeedInRpm), "NA;" },
                //{ nameof(Combine.YieldMeasurement), "on;off;" },
                //{ nameof(Combine.MoistureMeasurement), "on;off;" },
                //{ nameof(Combine.TypeOfCrop), "Sunflowers;Maize;" },
                //{ nameof(Combine.AutoPilotStatus), "on;off;" },
                //{ nameof(Combine.CruisePilotStatus), "0;" },
                //{ nameof(Combine.YieldInTonsPerHour), "NA;" },
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
                    tractor.Date = DateTime.Parse(entity[0]);
                    tractor.SerialNumber = entity[1];
                    tractor.GPSLongitude = double.Parse(entity[2]);
                    tractor.GPSLatitude = double.Parse(entity[3]);
                    tractor.TotalWorkingHours = double.Parse(entity[4]);
                    tractor.EngineSpeedInRpm = int.Parse(entity[5]);
                    tractor.EngineLoadInPercentage = double.Parse(entity[6]);
                    tractor.FuelConsumptionPerHour = entity[7] == "NA" ? null : double.Parse(entity[7]);
                    tractor.GroundSpeedGearboxInKmh = double.Parse(entity[8]);
                    tractor.GroundSpeedRadarInKmh = entity[9] == "NA" ? null : int.Parse(entity[9]);
                    tractor.CoolantTemperatureInCelsius = int.Parse(entity[10]);
                    tractor.SpeedFrontPtoInRpm = int.Parse(entity[11]);
                    tractor.SpeedRearPtoInRpm = int.Parse(entity[12]);
                    tractor.CurrentGearShift = entity[13] == "NA" ? null : short.Parse(entity[13]);
                    tractor.AmbientTemperatureInCelsius = double.Parse(entity[14]);
                    tractor.ParkingBreakStatus = ParseParkingBreakStatus(entity[15]);
                    tractor.TransverseDifferentialLockStatus = ParseTransverseDifferentialLockStatus(entity[16]);
                    tractor.AllWheelDriveStatus = ParseWheelDriveStatus(entity[17]);
                    tractor.ActualStatusOfCreeper = entity[18] == "Active" ? true : false;
                }
                catch (Exception e)
                {

                    throw;
                }

                tractorsTelemetry.Add(tractor);
            }

            return tractorsTelemetry;
        }

        private ParkingBreakStatus ParseParkingBreakStatus(string value)
        {
            if (_columnsWithSpecialValues.TryGetValue(nameof(TractorTelemetry.ParkingBreakStatus), out var val))
            {
                foreach (ParkingBreakStatus e in Enum.GetValues(typeof(ParkingBreakStatus)))
                {
                    if (e.GetDescription() == value)
                        return e;
                }
            }

            throw new ArgumentException();
        }

        private TransverseDifferentialLockStatus ParseTransverseDifferentialLockStatus(string value)
        {
            if (_columnsWithSpecialValues.TryGetValue(nameof(TractorTelemetry.TransverseDifferentialLockStatus), out var val))
            {
                foreach (TransverseDifferentialLockStatus e in Enum.GetValues(typeof(TransverseDifferentialLockStatus)))
                {
                    if (e.GetDescription() == value)
                        return e;
                }
            }

            throw new ArgumentException();
        }

        private WheelDriveStatus ParseWheelDriveStatus(string value)
        {
            if (_columnsWithSpecialValues.TryGetValue(nameof(TractorTelemetry.AllWheelDriveStatus), out var val))
            {
                foreach (WheelDriveStatus e in Enum.GetValues(typeof(WheelDriveStatus)))
                {
                    if (e.GetDescription() == value)
                        return e;
                }
            }

            throw new ArgumentException();
        }
    }
}
