using System;
using System.ComponentModel;

namespace SynologyDotNet.Core.Helpers
{
    /// <summary>
    /// Enum helper
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets the 'Description' attribute value
        /// </summary>
        public static string GetEnumDescription<TEnum>(TEnum value)
            where TEnum : Enum
        {
            var fieldInfo = typeof(TEnum).GetField(value.ToString());
            var attributes = fieldInfo.GetCustomAttributes(false);
            if (attributes.Length > 0)
                return (attributes[0] as DescriptionAttribute)?.Description;
            return value.ToString();
        }

        /// <summary>
        /// Gets the 'Description' attribute value
        /// </summary>
        public static string GetEnumDescription<TEnum>(int value)
            where TEnum : Enum
        {
            if (Enum.IsDefined(typeof(TEnum), value))
                return GetEnumDescription<TEnum>((TEnum)(object)value);
            return string.Empty;
        }
    }
}
