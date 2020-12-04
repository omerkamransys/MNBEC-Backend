using System;
using System.ComponentModel;
using System.Linq;

namespace MNBEC.Core.Extensions
{
    /// <summary>
    /// EnumExtensions contains extensions for Enumerations.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Description returns value of DescriptionAttribute.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Description(this Enum value)
        {
            // get attributes  
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // return description
            return attributes.Any() ? ((DescriptionAttribute)attributes.ElementAt(0)).Description : string.Empty;
        }
    }
}