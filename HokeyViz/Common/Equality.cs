using System.Collections.Generic;

namespace HokeyViz.Common
{
    static class Equality
    {
        public static bool Equal<T>(T first, T second)
        {
            return EqualityComparer<T>.Default.Equals(first, second);
        }
    }
}
