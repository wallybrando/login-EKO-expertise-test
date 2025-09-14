using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Extensions
{
    public static class ConvertExtensions
    {
        public static object? ChangeTypeNullable(string? value, Type type)
        {
            var actualType = Nullable.GetUnderlyingType(type) ?? type;
            return value == null ? null : Convert.ChangeType(value, actualType);
        }

        public static bool IsNullableType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }
    }
}
