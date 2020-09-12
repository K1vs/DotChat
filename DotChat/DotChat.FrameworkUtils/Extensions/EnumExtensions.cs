using System;
using System.Collections.Generic;
using System.Text;

namespace K1vs.DotChat.FrameworkUtils.Extensions
{
    public static class EnumExtensions
    {
        public static T GetCustomAttribute<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}
