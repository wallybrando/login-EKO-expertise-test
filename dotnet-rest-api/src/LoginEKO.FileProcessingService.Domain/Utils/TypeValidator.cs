using System.Linq.Expressions;

namespace LoginEKO.FileProcessingService.Domain.Utils
{
    public static class TypeValidator
    {
        public static bool IsNullableType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        public static object? ChangeTypeNullable(string? value, Type type)
        {
            var actualType = Nullable.GetUnderlyingType(type) ?? type;
            return value == null ? null : Convert.ChangeType(value, actualType);
        }

        public static bool FilterIsNumber(Type type)
        {
            return type == typeof(int) || type == typeof(double) || type == typeof(short) ||
                type == typeof(short?) || type == typeof(int?) || type == typeof(double?);
        }

        public static bool FilterIsDate(Type type)
        {
            return type == typeof(DateTime);
        }


        public static bool FilterIsString(Type type, ConstantExpression expression)
        {
            return type == typeof(string);
        }
    }
}
