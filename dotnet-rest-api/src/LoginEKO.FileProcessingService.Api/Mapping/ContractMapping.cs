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
                SerialNumber = DataHelper.ConvertToString(telemetry.SerialNumber),
                Date = DataHelper.DateTimeToString(telemetry.Date),
                GPSLongitude = DataHelper.DoubleToString(telemetry.GPSLongitude),
                GPSLatitude = DataHelper.DoubleToString(telemetry.GPSLatitude),
                TotalWorkingHours = DataHelper.DoubleToString(telemetry.TotalWorkingHours),
                EngineSpeedInRpm = DataHelper.IntToString(telemetry.EngineSpeedInRpm),
                EngineLoadInPercentage = DataHelper.DoubleToString(telemetry.EngineLoadInPercentage),
                FuelConsumptionPerHour = DataHelper.NullableDoubleToString(telemetry.FuelConsumptionPerHour),
                GroundSpeedGearboxInKmh = DataHelper.DoubleToString(telemetry.GroundSpeedGearboxInKmh),
                GroundSpeedRadarInKmh = DataHelper.NullableIntToString(telemetry.GroundSpeedRadarInKmh),
                CoolantTemperatureInCelsius = DataHelper.IntToString(telemetry.CoolantTemperatureInCelsius),
                SpeedFrontPtoInRpm = DataHelper.IntToString(telemetry.SpeedFrontPtoInRpm),
                SpeedRearPtoInRpm = DataHelper.IntToString(telemetry.SpeedRearPtoInRpm),
                CurrentGearShift = DataHelper.NullableShortToString(telemetry.CurrentGearShift),
                AmbientTemperatureInCelsius = DataHelper.DoubleToString(telemetry.AmbientTemperatureInCelsius),
                ParkingBreakStatus = DataHelper.EnumToString(telemetry.ParkingBreakStatus),
                TransverseDifferentialLockStatus = DataHelper.EnumToString(telemetry.TransverseDifferentialLockStatus),
                AllWheelDriveStatus = DataHelper.EnumToString(telemetry.AllWheelDriveStatus),
                ActualStatusOfCreeper = DataHelper.NullableBoolToString(telemetry.ActualStatusOfCreeper, "Active", "Inactive")
            };
        }

        public static IEnumerable<TractorTelemetryResponse> MapToResponse(this IEnumerable<TractorTelemetry> telemetries)
        {
            return telemetries.Select(MapToResponse);
        }
    }
}
