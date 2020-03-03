namespace K1vs.DotChat.FrameworkUtils.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class ReflectionExtensions
    {
        public static IReadOnlyCollection<Type> GetBaseClasses(this Type type, bool includeSelf = false)
        {
            if (type == null)
            {
                throw new NullReferenceException(nameof(type));
            }
            var result = new List<Type>();
            if (includeSelf)
            {
                result.Add(type);
            }
            while (type != typeof(object))
            {
                type = type.BaseType;
                result.Add(type);
            }
            return result;
        }
    }
}
