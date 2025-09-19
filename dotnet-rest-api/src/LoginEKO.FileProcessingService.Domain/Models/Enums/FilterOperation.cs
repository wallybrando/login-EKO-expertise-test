using System.ComponentModel;

namespace LoginEKO.FileProcessingService.Domain.Models.Enums
{
    [Flags]
    public enum FilterOperation
    {
        [Description("Equals")]
        EQUALS = 1,

        [Description("LessThan")]
        LESSTHAN,

        [Description("GreaterThan")]
        GREATERTHAN,

        [Description("Contains")]
        CONTAINS,

        UNKNOWN
    }
}
