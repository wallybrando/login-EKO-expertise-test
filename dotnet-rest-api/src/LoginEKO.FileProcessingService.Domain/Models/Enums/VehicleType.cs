using System.ComponentModel;

namespace LoginEKO.FileProcessingService.Domain.Models.Enums
{
    public enum VehicleType
    {
        [Description("LD_A")]
        TRACTOR = 1,
        [Description("LD_C")]
        COMBINE = 2,
        UNKNOWN
    }
}