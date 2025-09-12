using LoginEKO.FileProcessingService.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<bool> ImportTelemetryAsync(FileMetadata file);
    }
}
