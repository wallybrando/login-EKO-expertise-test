using System.ComponentModel;
using System.Reflection;

namespace LoginEKO.FileProcessingService.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null)
                throw new ArgumentNullException(nameof(fieldInfo));


            if (fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Length != 0)
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
