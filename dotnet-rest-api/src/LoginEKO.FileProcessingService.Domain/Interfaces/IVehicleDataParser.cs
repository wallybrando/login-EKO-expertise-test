using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;

namespace LoginEKO.FileProcessingService.Domain.Interfaces
{
    public interface IVehicleDataParser
    {
        VehicleType Type { get; init; }
        IEnumerable<VehicleTelemetry> TransformVehicleData(IEnumerable<string[]> data);
    }
}
