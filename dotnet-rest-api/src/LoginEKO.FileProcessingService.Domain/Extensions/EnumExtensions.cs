using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo? fi = value.GetType()
                .GetField(value.ToString());

            if (fi == null)
                throw new ArgumentNullException(nameof(fi));


            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Length != 0)
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
