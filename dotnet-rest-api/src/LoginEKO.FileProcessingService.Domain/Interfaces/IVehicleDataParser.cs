using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces
{
    public interface IVehicleDataParser
    {
        VehicleType Type { get; init; }
        IEnumerable<Vehicle> TransformVehicleData(IEnumerable<string[]> data);
    }
}
