using System.ComponentModel;
using System.Linq;

namespace Models.Enum
{
    public static class Extensions
    {
        public static string GetEnumDescription<TEnum>(this TEnum item)
        {
            return item.GetType()
                       .GetField(item.ToString())
                       .GetCustomAttributes(typeof(DescriptionAttribute), false)
                       .Cast<DescriptionAttribute>()
                       .FirstOrDefault()?.Description ?? string.Empty;
        }
    }
}