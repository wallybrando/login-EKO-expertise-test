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

        public static bool? ConvertToNullableBool(string field, string expectedTrue, string expectedFalse)
        {
            if (field == "NA")
                return null;

            return ConvertToBool(field, expectedTrue, expectedFalse);
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

        public static double ConvertToDouble(string field)
        {
            if (!double.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to double");

            return result;
        }

        public static double? ConvertToNullableDouble(string field)
        {
            if (field == "NA")
                return null;

            return ConvertToDouble(field);
        }

        public static T ParseEnumValue<T>(string columnValue) where T : struct, Enum
        {
            if (!Enum.TryParse(typeof(T), columnValue, true, out var value))
                throw new ArgumentException("Value cannot be converted to enum");

            return (T)value;
        }

        public static int? ConvertToNullableInt(string field)
        {
            if (field == "NA")
                return null;

            return ConvertToInt(field);
        }

        public static int ConvertToInt(string field)
        {
            if (!int.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to int");

            return result;
        }

        public static short ConvertToShort(string field)
        {
            if (!short.TryParse(field, out var result))
                throw new ArgumentException("Value cannot be converted to short");

            return result;
        }

        public static short? ConvertToNullabeShort(string field)
        {
            if (field == "NA")
                return null;

            return ConvertToShort(field);
        }
    }
}
