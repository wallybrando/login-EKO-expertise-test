using LoginEKO.FileProcessingService.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Utils
{
    public static class DataHelper
    {
        public static bool ConvertToBool(string field, string expectedTrue, string expectedFalse)
        {
            if (field.Equals(expectedTrue, StringComparison.OrdinalIgnoreCase))
                return true;
            if (field.Equals(expectedFalse, StringComparison.OrdinalIgnoreCase))
                return false;

            throw new ArgumentException("Value cannot be coverted to bool");
        }

        public static string BoolToString(bool value, string expectedTrue, string expectedFalse)
        {
            return value ? expectedTrue : expectedFalse;
        }

        public static bool? ConvertToNullableBool(string field, string expectedTrue, string expectedFalse)
        {
            if (field == "NA")
                return null;

            return ConvertToBool(field, expectedTrue, expectedFalse);
        }

        public static string NullableBoolToString(bool? value, string expectedTrue, string expectedFalse)
        {
            if (value == null)
                return "NA";

            return BoolToString(value.Value, expectedTrue, expectedFalse);
        }

        public static string ConvertToString(string field)
        {
            if (string.IsNullOrEmpty(field))
                throw new ArgumentException("Value cannot be converted to string");

            return field;
        }

        public static DateTime ConvertToDateTime(string field)
        {
            if (!DateTime.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to DateTime");

            return result;
        }

        public static string DateTimeToString(DateTime date, string? format = null)
        {
            if (string.IsNullOrEmpty(format))
                format = "yyyy-MM-dd hh:mm:ss";

            return date.ToString(format);
        }

        public static double ConvertToDouble(string field)
        {
            if (!double.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to double");

            return result;
        }

        public static string DoubleToString(double value)
        {
            return value.ToString();
        }

        public static double? ConvertToNullableDouble(string field)
        {
            if (field == "NA")
                return null;

            return ConvertToDouble(field);
        }

        public static string NullableDoubleToString(double? value)
        {
            if (value == null)
                return "NA";

            return DoubleToString(value.Value);
        }

        public static T ConvertToEnum<T>(string columnValue) where T : struct, Enum
        {
            if (!Enum.TryParse(typeof(T), columnValue, true, out var value))
                throw new ArgumentException("Value cannot be converted to enum");

            return (T)value;
        }

        public static string EnumToString<T>(T value) where T : struct, Enum
        {
            return value.GetDescription();
        }

        public static int? ConvertToNullableInt(string field)
        {
            if (field == "NA")
                return null;

            return ConvertToInt(field);
        }

        public static string NullableIntToString(int? value)
        {
            if (value == null)
                return "NA";

            return IntToString(value.Value);
        }

        public static int ConvertToInt(string field)
        {
            if (!int.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to int");

            return result;
        }

        public static string IntToString(int value)
        {
            return value.ToString();
        }

        public static short ConvertToShort(string field)
        {
            if (!short.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to short");

            return result;
        }

        public static string ShortToString(short value)
        {
            return value.ToString();
        }

        public static short? ConvertToNullabeShort(string field)
        {
            if (field == "NA")
                return null;

            return ConvertToShort(field);
        }

        public static string NullableShortToString(short? value)
        {
            if (value == null)
                return "NA";

            return ShortToString(value.Value);
        }
    }
}
