using LoginEKO.FileProcessingService.Domain.Models.Entities.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;

namespace LoginEKO.FileProcessingService.Domain.Interfaces
{
    public interface IVehicleTelemetryParser
    {
        VehicleType Type { get; init; }
        IEnumerable<AgroVehicleTelemetry> ParseAgroVehicleTelemetry(IEnumerable<string[]> data);
    }
}
