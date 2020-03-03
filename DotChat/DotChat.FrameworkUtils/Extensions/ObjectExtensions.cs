namespace K1vs.DotChat.FrameworkUtils.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ObjectExtensions
    {
        public static bool In<TObj>(this TObj obj, params TObj[] other)
        {
            return other.Contains(obj);
        }

        public static bool NotIn<TObj>(this TObj obj, params TObj[] other)
        {
            return !obj.In(other);
        }

        public static bool In<TObj>(this TObj obj, IEqualityComparer<TObj> comparer, params TObj[] other)
        {
            return other.Contains(obj, comparer);
        }

        public static bool NotIn<TObj>(this TObj obj, IEqualityComparer<TObj> comparer, params TObj[] other)
        {
            return !obj.In(comparer, other);
        }
    }
}
