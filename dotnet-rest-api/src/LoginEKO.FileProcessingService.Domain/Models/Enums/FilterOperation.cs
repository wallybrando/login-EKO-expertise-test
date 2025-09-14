using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        GreaterThan,

        [Description("Contains")]
        CONTAINS,

        UNKNOWN
    }
}
