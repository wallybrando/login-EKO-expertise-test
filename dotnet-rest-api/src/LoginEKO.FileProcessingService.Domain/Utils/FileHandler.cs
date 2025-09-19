using LoginEKO.FileProcessingService.Domain.Extensions;
using LoginEKO.FileProcessingService.Domain.Models.Enums;

namespace LoginEKO.FileProcessingService.Domain.Utils
{
    public static class FileHandler
    {
        public static VehicleType GetVehicleTypeFromFilename(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return VehicleType.UNKNOWN;

            if (filename.StartsWith(VehicleType.TRACTOR.GetDescription(), StringComparison.Ordinal))
                return VehicleType.TRACTOR;
            if (filename.StartsWith(VehicleType.COMBINE.GetDescription(), StringComparison.Ordinal))
                return VehicleType.COMBINE;

            return VehicleType.UNKNOWN;
        }

        public static FileType GetFileTypeFromExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return FileType.UNKNOWN;

            extension = extension.Split('/').Last();
            if (extension.Equals(FileType.CSV.GetDescription(), StringComparison.OrdinalIgnoreCase))
                return FileType.CSV;

            return FileType.UNKNOWN;
        }
    }
}
