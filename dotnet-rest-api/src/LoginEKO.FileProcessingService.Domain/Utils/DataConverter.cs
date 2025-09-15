using LoginEKO.FileProcessingService.Domain.Extensions;

namespace LoginEKO.FileProcessingService.Domain.Utils
{
    public static class DataConverter
    {
        public static bool ToBool(string field, string expectedTrue, string expectedFalse)
        {
            if (field.Equals(expectedTrue, StringComparison.OrdinalIgnoreCase))
                return true;
            if (field.Equals(expectedFalse, StringComparison.OrdinalIgnoreCase))
                return false;

            throw new ArgumentException("Value cannot be coverted to bool");
        }

        public static bool? ToNullableBool(string field, string expectedTrue, string expectedFalse)
        {
            if (field == "NA")
                return null;

            return ToBool(field, expectedTrue, expectedFalse);
        }

        public static string ToString(string field)
        {
            if (string.IsNullOrEmpty(field))
                throw new ArgumentException("Value cannot be converted to string");

            return field;
        }

        public static DateTime ToDateTime(string field)
        {
            if (!DateTime.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to DateTime");

            return result;
        }

        public static double ToDouble(string field)
        {
            if (!double.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to double");

            return result;
        }

        public static double? ToNullableDouble(string field)
        {
            if (field == "NA")
                return null;

            return ToDouble(field);
        }

        public static T ToEnum<T>(string columnValue) where T : struct, Enum
        {
            if (!Enum.TryParse(typeof(T), columnValue, true, out var value))
                throw new ArgumentException("Value cannot be converted to enum");

            return (T)value;
        }

        public static int? ToNullableInt(string field)
        {
            if (field == "NA")
                return null;

            return ToInt(field);
        }

        public static int ToInt(string field)
        {
            if (!int.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to int");

            return result;
        }

        public static short ToShort(string field)
        {
            if (!short.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to short");

            return result;
        }

        public static short? ToNullabeShort(string field)
        {
            if (field == "NA")
                return null;

            return ToShort(field);
        }

        public static string ToString(bool value, string expectedTrue, string expectedFalse)
        {
            return value ? expectedTrue : expectedFalse;
        }

        public static string ToString(bool? value, string expectedTrue, string expectedFalse)
        {
            if (value == null)
                return "NA";

            return ToString(value.Value, expectedTrue, expectedFalse);
        }

        public static string ToString(DateTime date, string? format = null)
        {
            if (string.IsNullOrEmpty(format))
                format = "yyyy-MM-dd hh:mm:ss";

            return date.ToString(format);
        }

        public static string ToString(double value)
        {
            return value.ToString();
        }

        public static string ToString(double? value)
        {
            if (value == null)
                return "NA";

            return ToString(value.Value);
        }

        public static string EnumToString<T>(T value) where T : struct, Enum
        {
            return value.GetDescription();
        }

        public static string ToString(int? value)
        {
            if (value == null)
                return "NA";

            return ToString(value.Value);
        }

        public static string ToString(int value)
        {
            return value.ToString();
        }

        public static string ToString(short value)
        {
            return value.ToString();
        }

        public static string ToString(short? value)
        {
            if (value == null)
                return "NA";

            return ToString(value.Value);
        }
    }
}
