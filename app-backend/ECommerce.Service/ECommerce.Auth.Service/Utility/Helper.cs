using System.ComponentModel;
using System.Reflection;

namespace ECommerce.Auth.Service.Utility
{
    public static class Helper
    {
        public static string GetDescription(Enum property)
        {
            FieldInfo field = property.GetType().GetField(property.ToString())!;
            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>()!;
            return attribute?.Description ?? string.Empty;
        }
    }
}
