using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace OnlineShopWebApp.Helpers
{
    public class EnumHelper
    {
        public static string GetDisplayName(Enum value)
        {
            return value.GetType()
                            .GetMember(value.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}