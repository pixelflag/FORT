using UnityEngine;

public class Calculate
{
    public static Vector3 GetRandomVector3(float value)
    {
        return new Vector3(Random.Range(-value, value), Random.Range(-value, value), Random.Range(-value, value));
    }

    public static Vector2 GetRandomVector2(float value)
    {
        return new Vector2(Random.Range(-value, value), Random.Range(-value, value));
    }

    public static float Distance(Vector3 pos1, Vector3 pos2)
    {
        float tempX = pos1.x - pos2.x;
        float tempY = pos1.y - pos2.y;
        return Mathf.Sqrt(tempX * tempX + tempY * tempY);
    }

    public static Vector3 DigreeToVector(float digree, float distance)
    {
        float radian = digree * Mathf.PI / 180;
        return new Vector3(Mathf.Cos(radian) * distance, Mathf.Sin(radian) * distance, 0);
    }

    public static Vector3 RadianToVector(float radian, float distance)
    {
        return new Vector3(Mathf.Cos(radian) * distance, Mathf.Sin(radian) * distance, 0);
    }

    public static Vector3 PositionToNomaliseVector(Vector3 position, Vector3 targetPosition)
    {
        float radian = PositionToRadian(position, targetPosition);
        float distance = 1.0f;

        return new Vector3(Mathf.Cos(radian) * distance, Mathf.Sin(radian) * distance, 0);
    }

    public static float PositionToRadian(Vector3 position, Vector3 targetPosition)
    {
        float radian = Mathf.Atan2(targetPosition.y - position.y, targetPosition.x - position.x);
        if (radian < 0)
        {
            radian = radian + 2 * Mathf.PI;
        }

        return radian;
    }

    public static Vector3 MiddlePosition(Vector3 position1, Vector3 position2)
    {
        return ((position2 - position1) / 2) + position1;
    }


    public static float RoundPoint3(float value)
    {
        value = value * 1000;
        value = Mathf.Round(value);
        value = value / 1000;

        return value;
    }

    public static float RoundPoint2(float value)
    {
        value = value * 100;
        value = Mathf.Round(value);
        value = value / 100;

        return value;
    }

    public static float AddRadian(float origin, float addValue)
    {
        origin += addValue;

        if (origin > Mathf.PI * 2)
        {
            origin -= Mathf.PI * 2;
        }
        else if (origin < 0)
        {
            origin += Mathf.PI * 2;
        }

        return origin;
    }

    public static int CheckSign(float value)
    {
        if (value < 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

}
