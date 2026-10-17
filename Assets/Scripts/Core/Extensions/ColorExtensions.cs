using UnityEngine;

namespace Core.Extensions
{
    public static class ColorExtensions
    {
        public static Color WithAlpha01(this Color color, float val)
        {
            color.a = val;
            return color;
        }
    }
}