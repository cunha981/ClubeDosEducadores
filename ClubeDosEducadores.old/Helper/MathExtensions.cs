using System;

namespace Helper
{
    public static class MathExtensions
    {
        public static double ToRadians(this double value)
        {
            return (Math.PI / 180) * value;
        }
    }
}
