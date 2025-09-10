using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Services.DataTransformators
{
    public class CombineDataTransformator : IVehicleDataTransformator
    {
        public VehicleType Type { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

        public IEnumerable<Vehicle> TransformVehicleData(IEnumerable<string[]> data)
        {
            var combineTelemetry = new List<Combine>();

            return combineTelemetry;
        }
    }
}
