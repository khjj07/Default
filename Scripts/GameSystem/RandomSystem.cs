using System;
using System.Collections.Generic;
using System.Linq;

namespace KCoreKit
{
    public static class RandomSystem
    {
        private static Random random;

        public static void SetSeed(int seed = 0)
        {
            random = new Random(seed);
        }
        
        public static T GetRandomElement<T>(this IList<T> array)
        {
            if (array == null || array.Count == 0)
            {
                return default;
            }
            return array.OrderBy(x => random.Next()).First();
        }

        public static float Range(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }
        
        public static int RangeInt(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}