using UnityEngine;

public class Easing
{
    public static float QuadIn(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime;
        return max * t * t + min;
    }

    public static float QuadOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime;
        return -max * t * (t - 2) + min;
    }

    public static float QuadInOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * t * t + min;

        t = t - 1;
        return -max / 2 * (t * (t - 2) - 1) + min;
    }

    public static float CubicIn(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime;
        return max * t * t * t + min;
    }

    public static float CubicOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t = t / totaltime - 1;
        return max * (t * t * t + 1) + min;
    }

    public static float CubicInOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * t * t * t + min;

        t = t - 2;
        return max / 2 * (t * t * t + 2) + min;
    }

    public static float QuartIn(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime;
        return max * t * t * t * t + min;
    }

    public static float QuartOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t = t / totaltime - 1;
        return -max * (t * t * t * t - 1) + min;
    }

    public static float QuartInOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * t * t * t * t + min;

        t = t - 2;
        return -max / 2 * (t * t * t * t - 2) + min;
    }

    public static float QuintIn(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime;
        return max * t * t * t * t * t + min;
    }

    public static float QuintOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t = t / totaltime - 1;
        return max * (t * t * t * t * t + 1) + min;
    }

    public static float QuintInOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * t * t * t * t * t + min;

        t = t - 2;
        return max / 2 * (t * t * t * t * t + 2) + min;
    }

    public static float SineIn(float t, float totaltime, float min, float max)
    {
        max -= min;
        return -max * Mathf.Cos(t * (Mathf.PI * 90 / 180) / totaltime) + max + min;
    }

    public static float SineOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        return max * Mathf.Sin(t * (Mathf.PI * 90 / 180) / totaltime) + min;
    }

    public static float SineInOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        return -max / 2 * (Mathf.Cos(t * Mathf.PI / totaltime) - 1) + min;
    }

    public static float ExpIn(float t, float totaltime, float min, float max)
    {
        max -= min;
        return t == 0.0 ? min : max * Mathf.Pow(2, 10 * (t / totaltime - 1)) + min;
    }

    public static float ExpOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        return t == totaltime ? max + min : max * (-Mathf.Pow(2, -10 * t / totaltime) + 1) + min;
    }

    public static float ExpInOut(float t, float totaltime, float min, float max)
    {
        if (t == 0.0f) return min;
        if (t == totaltime) return max;
        max -= min;
        t /= totaltime / 2;

        if (t < 1) return max / 2 * Mathf.Pow(2, 10 * (t - 1)) + min;

        t = t - 1;
        return max / 2 * (-Mathf.Pow(2, -10 * t) + 2) + min;

    }

    public static float CircIn(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime;
        return -max * (Mathf.Sqrt(1 - t * t) - 1) + min;
    }

    public static float CircOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t = t / totaltime - 1;
        return max * Mathf.Sqrt(1 - t * t) + min;
    }

    public static float CircInOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime / 2;
        if (t < 1) return -max / 2 * (Mathf.Sqrt(1 - t * t) - 1) + min;

        t = t - 2;
        return max / 2 * (Mathf.Sqrt(1 - t * t) + 1) + min;
    }

    public static float ElasticIn(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime;

        float s = 1.70158f;
        float p = totaltime * 0.3f;
        float a = max;

        if (t == 0) return min;
        if (t == 1) return min + max;

        if (a < Mathf.Abs(max))
        {
            a = max;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(max / a);
        }

        t = t - 1;
        return -(a * Mathf.Pow(2, 10 * t) * Mathf.Sin((t * totaltime - s) * (2 * Mathf.PI) / p)) + min;
    }

    public static float ElasticOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime;

        float s = 1.70158f;
        float p = totaltime * 0.3f; ;
        float a = max;

        if (t == 0) return min;
        if (t == 1) return min + max;

        if (a < Mathf.Abs(max))
        {
            a = max;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(max / a);
        }

        return a * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * totaltime - s) * (2 * Mathf.PI) / p) + max + min;
    }

    public static float ElasticInOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime / 2;

        float s = 1.70158f;
        float p = totaltime * (0.3f * 1.5f);
        float a = max;

        if (t == 0) return min;
        if (t == 2) return min + max;

        if (a < Mathf.Abs(max))
        {
            a = max;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(max / a);
        }

        if (t < 1)
        {
            return -0.5f * (a * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * totaltime - s) * (2 * Mathf.PI) / p)) + min;
        }

        t = t - 1;
        return a * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * totaltime - s) * (2 * Mathf.PI) / p) * 0.5f + max + min;
    }

    public static float BackIn(float t, float totaltime, float min, float max, float s)
    {
        max -= min;
        t /= totaltime;
        return max * t * t * ((s + 1) * t - s) + min;
    }

    public static float BackOut(float t, float totaltime, float min, float max, float s)
    {
        max -= min;
        t = t / totaltime - 1;
        return max * (t * t * ((s + 1) * t + s) + 1) + min;
    }

    public static float BackInOut(float t, float totaltime, float min, float max, float s)
    {
        max -= min;
        s *= 1.525f;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * (t * t * ((s + 1) * t - s)) + min;

        t = t - 2;
        return max / 2 * (t * t * ((s + 1) * t + s) + 2) + min;
    }

    public static float BounceIn(float t, float totaltime, float min, float max)
    {
        max -= min;
        return max - BounceOut(totaltime - t, totaltime, 0, max) + min;
    }

    public static float BounceOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime;

        if (t < 1.0f / 2.75f)
        {
            return max * (7.5625f * t * t) + min;
        }
        else if (t < 2.0f / 2.75f)
        {
            t -= 1.5f / 2.75f;
            return max * (7.5625f * t * t + 0.75f) + min;
        }
        else if (t < 2.5f / 2.75f)
        {
            t -= 2.25f / 2.75f;
            return max * (7.5625f * t * t + 0.9375f) + min;
        }
        else
        {
            t -= 2.625f / 2.75f;
            return max * (7.5625f * t * t + 0.984375f) + min;
        }
    }

    public static float BounceInOut(float t, float totaltime, float min, float max)
    {
        if (t < totaltime / 2)
        {
            return BounceIn(t * 2, totaltime, 0, max - min) * 0.5f + min;
        }
        else
        {
            return BounceOut(t * 2 - totaltime, totaltime, 0, max - min) * 0.5f + min + (max - min) * 0.5f;
        }
    }

    public static float Linear(float t, float totaltime, float min, float max)
    {
        return (max - min) * t / totaltime + min;
    }

    public static float ForType(EasingType type, float t, float totalTime, float min, float max)
    {
        switch (type)
        {
            case EasingType.Constant:
                return max;
            case EasingType.QuadIn:
                return Easing.QuadIn(t, totalTime, min, max);
            case EasingType.QuadOut:
                return Easing.QuadOut(t, totalTime, min, max);
            case EasingType.QuadInOut:
                return Easing.QuadInOut(t, totalTime, min, max);
            case EasingType.CubicIn:
                return Easing.CubicIn(t, totalTime, min, max);
            case EasingType.CubicOut:
                return Easing.CubicOut(t, totalTime, min, max);
            case EasingType.CubicInOut:
                return Easing.CubicInOut(t, totalTime, min, max);
            case EasingType.QuartIn:
                return Easing.QuartIn(t, totalTime, min, max);
            case EasingType.QuartOut:
                return Easing.QuartOut(t, totalTime, min, max);
            case EasingType.QuartInOut:
                return Easing.QuartInOut(t, totalTime, min, max);
            case EasingType.QuintIn:
                return Easing.QuintIn(t, totalTime, min, max);
            case EasingType.QuintOut:
                return Easing.QuintOut(t, totalTime, min, max);
            case EasingType.QuintInOut:
                return Easing.QuintInOut(t, totalTime, min, max);
            case EasingType.SineIn:
                return Easing.SineIn(t, totalTime, min, max);
            case EasingType.SineOut:
                return Easing.SineOut(t, totalTime, min, max);
            case EasingType.SineInOut:
                return Easing.SineInOut(t, totalTime, min, max);
            case EasingType.ExpIn:
                return Easing.ExpIn(t, totalTime, min, max);
            case EasingType.ExpOut:
                return Easing.ExpOut(t, totalTime, min, max);
            case EasingType.ExpInOut:
                return Easing.ExpInOut(t, totalTime, min, max);
            case EasingType.CircIn:
                return Easing.CircIn(t, totalTime, min, max);
            case EasingType.CircOut:
                return Easing.CircOut(t, totalTime, min, max);
            case EasingType.CircInOut:
                return Easing.CircInOut(t, totalTime, min, max);
            case EasingType.ElasticIn:
                return Easing.ElasticIn(t, totalTime, min, max);
            case EasingType.ElasticOut:
                return Easing.ElasticOut(t, totalTime, min, max);
            case EasingType.ElasticInOut:
                return Easing.ElasticInOut(t, totalTime, min, max);
            case EasingType.BackIn:
                return Easing.BackIn(t, totalTime, min, max, 1.7f);
            case EasingType.BackOut:
                return Easing.BackOut(t, totalTime, min, max, 1.7f);
            case EasingType.BackInOut:
                return Easing.BackInOut(t, totalTime, min, max, 1.7f);
            case EasingType.BounceIn:
                return Easing.BounceIn(t, totalTime, min, max);
            case EasingType.BounceOut:
                return Easing.BounceOut(t, totalTime, min, max);
            case EasingType.BounceInOut:
                return Easing.BounceInOut(t, totalTime, min, max);
            case EasingType.Linear:
                return Easing.Linear(t, totalTime, min, max);
        }
        return max;
    }
}