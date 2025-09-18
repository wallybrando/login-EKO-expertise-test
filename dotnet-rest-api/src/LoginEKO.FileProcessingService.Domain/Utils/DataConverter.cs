using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Extensions;
using System.Drawing;

namespace LoginEKO.FileProcessingService.Domain.Utils
{
    public static class DataConverter
    {
        public static bool ToBool(string value, string expectedTrue, string expectedFalse)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(expectedTrue) || string.IsNullOrEmpty(expectedFalse))
                throw new ArgumentException("Value cannot be empty");

            if (string.IsNullOrEmpty(expectedTrue))
                throw new ArgumentException("ExpectedTrue value cannot be empty");

            if (string.IsNullOrEmpty(expectedFalse))
                throw new ArgumentException("ExpectedFalse value cannot be empty");

            if (value.Equals(expectedTrue, StringComparison.OrdinalIgnoreCase))
                return true;
            if (value.Equals(expectedFalse, StringComparison.OrdinalIgnoreCase))
                return false;

            throw new DataConversionException("Value cannot be coverted to bool");
        }

        public static bool? ToNullableBool(string field, string expectedTrue, string expectedFalse)
        {
            if (field == "NA")
                return null;

            return ToBool(field, expectedTrue, expectedFalse);
        }

        public static string ToString(string value)
        {
            return value;
        }

        public static DateTime ToDateTime(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be empty");

            if (!DateTime.TryParse(value, out var result))
                throw new DataConversionException("Value cannot be converted to DateTime");

            return result;
        }

        public static double ToDouble(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be empty");

            if (!double.TryParse(value, out var result))
                throw new DataConversionException("Value cannot be converted to double");

            return result;
        }

        public static double? ToNullableDouble(string value)
        {
            if (value == "NA")
                return null;

            return ToDouble(value);
        }

        public static T ToEnum<T>(string value) where T : struct, Enum
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be empty");

            if (!Enum.TryParse(typeof(T), value, true, out var result))
                throw new DataConversionException("Value cannot be converted to enum");

            if (!Enum.IsDefined(typeof(T), result))
                throw new DataConversionException($"Value '{result}' is not valid enum value");
            return (T)result;
        }

        public static int? ToNullableInt(string value)
        {
            if (value == "NA")
                return null;

            return ToInt(value);
        }

        public static int ToInt(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be empty");

            if (!int.TryParse(value, out var result))
                throw new DataConversionException("Value cannot be converted to int");

            return result;
        }

        public static short ToShort(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be empty");

            if (!short.TryParse(value, out var result))
                throw new DataConversionException("Value cannot be converted to short");

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
            if (string.IsNullOrEmpty(expectedTrue))
                throw new ArgumentException("ExpectedTrue value cannot be empty");

            if (string.IsNullOrEmpty(expectedFalse))
                throw new ArgumentException("ExpectedFalse value cannot be empty");

            return value ? expectedTrue : expectedFalse;
        }

        public static string ToString(bool? value, string expectedTrue, string expectedFalse)
        {
            if (value == null)
                return "NA";

            return ToString(value.Value, expectedTrue, expectedFalse);
        }

        public static string ToString(DateTime date, string? format = "G")
        {
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
