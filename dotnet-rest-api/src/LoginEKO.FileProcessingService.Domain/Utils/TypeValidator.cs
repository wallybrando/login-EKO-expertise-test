namespace LoginEKO.FileProcessingService.Domain.Utils
{
    public static class TypeValidator
    {
        public static bool IsNullableType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        public static object? ChangeTypeNullable(object? value, Type type)
        {
            var actualType = Nullable.GetUnderlyingType(type) ?? type;
            return value == null ? null : Convert.ChangeType(value, actualType);
        }
    }
}
