using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarsAbove.Utilities;

internal static class EaseHelper
{
    public static float InQuad(float t) => t * t;
    public static float OutQuad(float t) => 1 - InQuad(1 - t);
    public static float InOutQuad(float t)
    {
        if (t < 0.5) return InQuad(t * 2) / 2;
        return 1 - InQuad((1 - t) * 2) / 2;
    }

    public static float EaseIn(float t)
    {
        return t * t;
    }
    static float Flip(float x)
    {
        return 1 - x;
    }
    public static float EaseOut(float t)
    {
        return Flip((float)Math.Sqrt(Flip(t)));
    }
    public static float EaseInOut(float t)
    {
        return MathHelper.Lerp(EaseIn(t), EaseOut(t), t);
    }

}