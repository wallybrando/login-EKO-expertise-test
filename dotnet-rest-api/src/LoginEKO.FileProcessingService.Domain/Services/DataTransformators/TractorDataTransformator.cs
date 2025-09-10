using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;

namespace LoginEKO.FileProcessingService.Domain.Services.DataTransformators
{
    public class TractorDataTransformator : IVehicleDataTransformator
    {
        public VehicleType Type { get; init; }

        public TractorDataTransformator()
        {
            Type = VehicleType.TRACTOR;
        }

        public IEnumerable<Vehicle> TransformVehicleData(IEnumerable<string[]> data)
        {
            var tractorsTelemetry = new List<Tractor>();
            foreach (var entity in data)
            {
                var tractor = new Tractor();

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
                    //tractor.//ParkingBreakStatus = actualData[15],
                    //tractor.//TransverseDifferentialLockStatus = actualData[16],
                    //tractor.//AllWheelDriveStatus = actualData[17],
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
    }
}
