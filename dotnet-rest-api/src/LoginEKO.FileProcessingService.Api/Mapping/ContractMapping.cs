using LoginEKO.FileProcessingService.Contracts.Requests;
using LoginEKO.FileProcessingService.Contracts.Responses;
using LoginEKO.FileProcessingService.Domain.Extensions;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;
using System.Runtime.CompilerServices;

namespace LoginEKO.FileProcessingService.Api.Mapping
{
    public static class ContractMapping
    {
        public static FileMetadata MapToFileMetadata(this UploadFileRequest request)
        {
            return new FileMetadata
            {
                Filename = request.File.FileName,
                Extension = request.File.ContentType.Split('/').Last(),
                SizeInBytes = request.File.Length,
                BinaryObject = ToByteArray(request.File),
                File = request.File,
            };
        }

        public static FileMetadataResponse MapToResponse(this FileMetadata file)
        {
            return new FileMetadataResponse
            {
                Id = file.Id,
                Filename = file.Filename,
                SizeInBytes = file.SizeInBytes,
                CreatedDate = file.CreatedDate,
            };
        }

        private static byte[] ToByteArray(IFormFile file)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }

        public static Filter MapToFilter(this FilterRequest request)
        {
            return new Filter
            {
                Field = request.Field.ToLower(),
                Operation = request.Operation ?? FilterOperation.EQUALS.GetDescription().ToLower(),
                Value = request.Value
            };
        }

        public static PaginatedFilter MapToPaginatedFilter(this IEnumerable<FilterRequest> filters)
        {
            return new PaginatedFilter
            {
                Filters = filters.Select(MapToFilter)
            };
        }

        public static TractorTelemetryResponse MapToResponse(this TractorTelemetry telemetry)
        {
            return new TractorTelemetryResponse
            {
                SerialNumber = DataConverter.ToString(telemetry.SerialNumber),
                Date = DataConverter.ToString(telemetry.Date),
                GPSLongitude = DataConverter.ToString(telemetry.GPSLongitude),
                GPSLatitude = DataConverter.ToString(telemetry.GPSLatitude),
                TotalWorkingHours = DataConverter.ToString(telemetry.TotalWorkingHours),
                EngineSpeedInRpm = DataConverter.ToString(telemetry.EngineSpeedInRpm),
                EngineLoadInPercentage = DataConverter.ToString(telemetry.EngineLoadInPercentage),
                FuelConsumptionPerHour = DataConverter.ToString(telemetry.FuelConsumptionPerHour),
                GroundSpeedGearboxInKmh = DataConverter.ToString(telemetry.GroundSpeedGearboxInKmh),
                GroundSpeedRadarInKmh = DataConverter.ToString(telemetry.GroundSpeedRadarInKmh),
                CoolantTemperatureInCelsius = DataConverter.ToString(telemetry.CoolantTemperatureInCelsius),
                SpeedFrontPtoInRpm = DataConverter.ToString(telemetry.SpeedFrontPtoInRpm),
                SpeedRearPtoInRpm = DataConverter.ToString(telemetry.SpeedRearPtoInRpm),
                CurrentGearShift = DataConverter.ToString(telemetry.CurrentGearShift),
                AmbientTemperatureInCelsius = DataConverter.ToString(telemetry.AmbientTemperatureInCelsius),
                ParkingBreakStatus = DataConverter.EnumToString(telemetry.ParkingBreakStatus),
                TransverseDifferentialLockStatus = DataConverter.EnumToString(telemetry.TransverseDifferentialLockStatus),
                AllWheelDriveStatus = DataConverter.EnumToString(telemetry.AllWheelDriveStatus),
                ActualStatusOfCreeper = DataConverter.ToString(telemetry.ActualStatusOfCreeper, "Active", "Inactive")
            };
        }

        public static IEnumerable<TractorTelemetryResponse> MapToResponse(this IEnumerable<TractorTelemetry> telemetries)
        {
            return telemetries.Select(MapToResponse);
        }

        public static CombineTelemetryResponse MapToResponse(this CombineTelemetry telemetry)
        {
            return new CombineTelemetryResponse();
            //return new CombineTelemetryResponse
            //{
            //    SerialNumber = Data
            //}
        }

        public static IEnumerable<CombineTelemetryResponse> MapToResponse(this IEnumerable<CombineTelemetry> telemetries)
        {
            return telemetries.Select(MapToResponse);
        }
    }
}
